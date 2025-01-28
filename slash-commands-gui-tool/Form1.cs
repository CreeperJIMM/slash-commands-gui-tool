using System.Globalization;
using DiscordAPI;
using SQLite;
using ConfigRW;
using ThemeSW;
using LocalFileRW;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

namespace slash_commands_gui_tool
{
    public partial class Form1 : Form
    {
        static SQLiteHelper sql = new SQLiteHelper();
        static DiscordAPIhelper discord = new DiscordAPIhelper();
        static ConfigHelper config = new ConfigHelper();
        static ThemeHelper theme = new ThemeHelper();
        static LocalFileHelper local = new LocalFileHelper();
        public static SimpleSlash[]? BotSlash;                                 //線上指令列表(不完整)
        public static List<SlashCommand> BotCache = new List<SlashCommand>();  //線上SlashCommand緩存區
        public static List<SlashCommand> LocalCache = new List<SlashCommand>();//本地SlashCommand緩存區
        public static SlashCommand? NowSlash;    //當前原始的指令
        public static CommandOption? NowOption;  //當前操作的選項(如果有)
        public static int NowFloor;              //當前選項層數
        public static bool Status;               //操作狀態(1線上/0本地)
        public static string USER_LANGUAGE = "en-US";
        public static void CreateDatabase()
        {
            if (sql.CreateDatabase()) {
                sql.CreateTable();
                sql.CreateData();
            }
        }
        public Form1()
        {
            theme.SetTheme(this);
            ChangeLanguage(USER_LANGUAGE);
            InitializeComponent();
        }
        public static void ChangeLanguage(string cultureName)
        {
            switch (cultureName) {
                case "zh-TW":
                    cultureName = "zh-Hant";
                    break;
                case "zh-CN":
                    cultureName = "zh-Hans";
                    break;
            }
            USER_LANGUAGE = cultureName;
            CultureInfo culture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Language
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
            string? lang = config.GetValue("General", "Language");
            if (lang == null || lang == "null") {
                USER_LANGUAGE = currentUICulture.Name;
                ChangeLanguage(currentUICulture.Name);
                config.SetValue("General", "Language", currentUICulture.Name);
            }
            else {
                USER_LANGUAGE = lang;
                ChangeLanguage(USER_LANGUAGE);
            }
            Controls.Clear();
            InitializeComponent();
            //AutoSync
            string? atsc = config.GetValue("General", "AutoSync");
            if (atsc != null) AutoSyncFunction(atsc);
            CreateDatabase();
            if (!sql.VerifyData()) {
                DialogResult result = MessageBox.Show($"{Resource.VerityError}\n{Resource.VerityError2}\n{Resource.VerityError3}", Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (result == DialogResult.OK) {
                    sql.DeleteDatabase();
                    CreateDatabase();
                    LoadClients();
                }
                else
                    Application.Exit();
            }
            else {
                LoadClients();
                await VerityValidClientAsync();
                await LoadCommands();
            }
        }

        public static List<Client>? clients;
        public static List<Client> caches = new List<Client>();
        public static List<int> pathIndex = new List<int>(); //option走訪路徑
        public static int SelectIndex = -1;
        Client? select;
        bool Operation = true;
        bool Changed = false;
        bool IsSave = true;
        bool AutoSync = true;
        private void LoadClients()
        {
            button1.Enabled = false;
            groupBox1.Enabled = false;
            clients = new List<Client>(sql.GetClients());
            clients.AddRange(caches);
            toolStripComboBox1.Items.Clear();
            if (clients.Count > 0) {
                foreach (Client client in clients) {
                    toolStripComboBox1.Items.Add(client.Name);
                }
                toolStripComboBox1.SelectedIndex = 0;
                select = clients.ToArray()[toolStripComboBox1.SelectedIndex];
                button1.Enabled = true;
                groupBox1.Enabled = true;
            }
        }
        private async Task VerityValidClientAsync()
        {
            toolStripStatusLabel1.Text = Resource.LoadingClient;
            toolStripProgressBar1.Value = 0;
            int count = 0;
            toolStripStatusLabel1.Text = $"{Resource.Connecting}";
            bool net = await discord.DetectNetwork();
            while (count < 10 && !net) {
                count++;
                toolStripProgressBar1.Value = count * 10;
                toolStripStatusLabel1.Text = $"{Resource.Connecting} {count}";
                net = await discord.DetectNetwork();
                await Task.Delay(new TimeSpan(0, 0, 0, 0, 500));
            }
            if (!net) {
                MessageBox.Show(Resource.NoNetwork, Resource.NetworkError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            toolStripStatusLabel1.Text = $"{Resource.ConnectionSuccessful}";
            await Task.Delay(new TimeSpan(0, 0, 0, 0, 100));
            count = 0;
            if (clients == null) return;
            foreach (Client client in clients) {
                DiscordUser? user = await discord.GetClientAsync(client);
                if (user == null) {
                    while (user == null) {
                        DialogResult result = MessageBox.Show(Resource.VerityClientError + $"\n\n{client.Name}: {client.ApplicationID}", Resource.ClientError, MessageBoxButtons.CancelTryContinue, MessageBoxIcon.Warning);
                        if (result == DialogResult.Continue) {
                            if (client.id == -1)
                                caches.Remove(client);
                            else
                                sql.DeleteClient(client.id);
                            break;
                        }
                        else if (result == DialogResult.TryAgain)
                            user = await discord.GetClientAsync(client);
                        else {
                            Application.Exit();
                            break;
                        }
                    }
                }
                count++;
                toolStripStatusLabel1.Text = $"{Resource.LoadingClient} {count}/{clients.Count}";
                toolStripProgressBar1.Value = count * (100 / clients.Count);
            }
            toolStripStatusLabel1.Text = $"{Resource.LoadClientComplete}";
            toolStripProgressBar1.Value = 100;
            LoadClients();
        }
        private async Task LoadCommands()
        {
            if (select == null) return;
            SimpleSlash[]? list = await discord.GetCommandsListAsync(select);
            if (list == null) return;
            list = list.OrderBy(x => x.name).ToArray();
            listBox1.Items.Clear();
            BotSlash = list;
            BotCache = new List<SlashCommand>();
            foreach (SimpleSlash slash in list) {
                listBox1.Items.Add($"{slash.name}");
            }
        }
        private void LoadLocalCommands()
        {
            listBox2.Items.Clear();
            foreach (SlashCommand slash in LocalCache) {
                listBox2.Items.Add($"{slash.name}");
            }
        }

        private async Task<SlashCommand?> LoadSingleCommandAsync(SimpleSlash simple)
        {
            SlashCommand? slash = BotCache.Find(s => s.id == simple.id);
            if (select == null || simple.id == null) return null;
            if (slash == null) {
                if (discord.IsRequestAllowed(500) != -1) return null;
                slash = await discord.GetCommandAsync(select, simple.id, toolStripProgressBar1);
            }
            if (slash == null) return null;
            BotCache.Add(slash);
            pathIndex.Clear();
            UpdateCommand(slash);
            checkBox3.Visible = true;
            return slash;
        }
        private void UpdateCommand(SlashCommand slash)
        {
            groupBox5.Controls.Clear();
            LinkLabel label = new LinkLabel();
            label.AutoSize = true;
            label.Text = $"/{slash.name}";
            label.Location = new Point(3, 10);
            label.Font = new Font("Arial", 12);
            groupBox5.Controls.Add(label);
            backbutton.Enabled = false;
            addoptionButton.Enabled = true;
            cmdnameTextbox.Text = lastname = slash.name;
            cmdnameTextbox.ReadOnly = true;
            cmddescTextbox.Text = lastdesc = slash.description;
            if (slash.nsfw != null) checkBox3.Checked = (bool)slash.nsfw;
            listBox3.Items.Clear();
            if (slash.options != null && slash.options.Count > 0) {
                slash.options = slash.options.OrderBy(item => item.name).OrderBy(item => item.type).ToList();
                for (int i = 0; i < slash.options.Count; i++) {
                    listBox3.Items.Add($"[{slash.options[i].type}] {slash.options[i].name}");
                }
            }
            UpdateType(slash, slash.type, true);
        }
        List<CommandType> tps;
        private void UpdateType(SlashCommand slash, int value, bool locked)
        {
            comboBox1.Items.Clear();
            List<CommandOption> options = GetPreviousOptions();
            int t = 0;
            t = slash.type;
            if (NowOption != null) t = NowOption.type;
            comboBox1.Items.Clear();
            tps = new List<CommandType>(types);
            if (NowFloor != 0) {
                if (IsHasSubCommnad(options)) {
                    tps.RemoveRange(2, types.Length - 2);
                }
                if (IsHasParameter(options)) {
                    tps.RemoveRange(0, 2);
                }
                //檢查後面的選項
                if (NowOption != null && NowOption.options != null) {
                    if (IsHasParameter(NowOption.options)) {//若後續選項有參數則不可選擇group command
                        tps.Remove(types[1]);
                    }
                    if (IsHasSubCommnad(NowOption.options)) {//若後續選項有子指令則不可選擇sub command
                        tps.Remove(types[0]);
                    }
                }
                if (NowFloor == 2) {
                    tps.Remove(types[1]);
                }
            }
            foreach (CommandType type in tps) {
                string patten = "";
                if (type.choice) patten = "*";
                comboBox1.Items.Add($"[{type.value}] {type.name}{patten}");
            }
            int index = tps.FindIndex(m => m.value == value);
            if (index != -1) comboBox1.SelectedIndex = index;
            if (locked) comboBox1.Enabled = false;
            else comboBox1.Enabled = true;
        }
        private bool IsHasSubCommnad(List<CommandOption> options)
        {
            bool flag = false;
            if (options == null) return false;
            foreach (CommandOption option in options) {
                if (option.type == 1 || option.type == 2) flag = true;
            }
            return flag;
        }
        private bool IsHasParameter(List<CommandOption> options)
        {
            bool flag = false;
            if (options == null) return false;
            foreach (CommandOption option in options) {
                if (option.type > 2) flag = true;
            }
            return flag;
        }
        private async Task<bool> SaveSingleCommand(SlashCommand slash)
        {
            if (select == null) return false;
            return await discord.PostCommandAsync(select, slash, toolStripProgressBar1);
        }

        private async void changeLanguage_Click(object sender, EventArgs e)
        {
            ToolStripItem? tool = sender as ToolStripItem;
            if (tool == null || tool.Tag == null) return;
            config.SetValue("General", "Language", (string)tool.Tag);
            USER_LANGUAGE = (string)tool.Tag;
            ChangeLanguage(USER_LANGUAGE);
            this.Controls.Clear();
            InitializeComponent();
            LoadClients();
            await VerityValidClientAsync();
            await LoadCommands();
        }

        private async void CreateClient_Click(object sender, EventArgs e)
        {
            this.Activate();
            Form form2 = new Form2();
            form2.StartPosition = FormStartPosition.CenterParent;
            if (form2.ShowDialog() == DialogResult.OK) {
                DiscordUser? user = Form2.User;
                string key = Form2.ClientToken;
                bool save = Form2.Saved;
                if (user == null || user.Id == null) return;
                if (save)
                    sql.InsertClient($"{user.Username}#{user.Discriminator}", user.Id, key, true);
                else
                    caches.Add(new Client(-1, $"{user.Username}#{user.Discriminator}", user.Id, key));

                LoadClients();
                if (clients != null && clients.Count > 0) {
                    await VerityValidClientAsync();
                    await LoadCommands();
                }
            }
        }
        private async void ClientSelected(object sender, EventArgs e)
        {
            if (clients == null) return;
            if (select != null) {
                Client? client = clients.ToArray()[toolStripComboBox1.SelectedIndex];
                if (select.ApplicationID != client.ApplicationID) {
                    select = client;
                    await LoadCommands();
                    listBox1.ClearSelected();
                    listBox2.ClearSelected();
                    listBox3.ClearSelected();
                    SelectIndex = -1;
                    BotCache.Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (select.id == -1)
                caches.Remove(select);
            else
                sql.DeleteClient(select.id);
            LoadClients();
        }
        private void autoSync_Click(object sender, EventArgs e)
        {
            ToolStripItem? tool = sender as ToolStripItem;
            if (tool == null || tool.Tag == null) return;
            AutoSyncFunction((string)tool.Tag);
            config.SetValue("General", "AutoSync", $"{(string)tool.Tag}");
        }
        private void AutoSyncFunction(string boolean)
        {
            if (boolean == "enable") {
                AutoSync = true;
                AutoSyncStripMenuItem.Text = $"\U00002705 {Resource.AutoSync} (&A)";
            }
            else if (boolean == "disable") {
                AutoSync = false;
                AutoSyncStripMenuItem.Text = $"\U0000274C {Resource.AutoSync} (&A)";
            }
        }
        private void ThemeMode_click(object sender, EventArgs e)
        {
            ToolStripItem? button = sender as ToolStripItem;
            if (button == null || button.Tag == null) return;
            config.SetValue("UserSettings", "Theme", $"{(string)button.Tag}");
            theme.SetTheme(this);
        }
        private async void listboxSelect(object sender, EventArgs e)
        {
            ListBox? listBox = sender as ListBox;
            if (listBox == null || listBox.Tag == null) return;
            string type = (string)listBox.Tag;
            if (listBox.SelectedIndex == -1) return;
            bool bothselect = listBox1.SelectedIndex != -1 && listBox2.SelectedIndex != -1;
            if (!Operation && NowSlash != null && (listBox.SelectedIndex != SelectIndex || bothselect)) {//檢測是否前一個指令還沒讀取就切換的情況
                listBox1.ClearSelected();
                listBox2.ClearSelected();
                if (Status)
                    listBox1.SelectedIndex = SelectIndex;
                else
                    listBox2.SelectedIndex = SelectIndex;
                return;
            }
            if (!Operation) return;
            if (!await ChangeEvent(listBox)) return;
            if (type == "Bot" && Status)
                if (listBox.SelectedIndex == SelectIndex) return;
            //更新status
            if (type == "Bot") {
                Status = true;
                listBox2.ClearSelected();
            }
            else if (type == "Local") {
                Status = false;
                listBox1.ClearSelected();
            }
            //更新listbox
            List<int> list = listBox.SelectedIndices.Cast<int>().ToList();
            if (list.Count == 1) {//單選
                Operation = false;
                plusbutton1.Enabled = true;
                minusbutton1.Enabled = true;
                groupBox3.Visible = true;
                groupBox3.Enabled = false;
                groupBox6.Visible = true;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = true;
                editchoiceButton.Visible = false;
                label1.Visible = false;
                Changed = false;
                SelectIndex = listBox.SelectedIndex;
                NowFloor = 0;
                NowOption = null;
                groupBox3.Enabled = true;
                if (Status) {
                    if (BotSlash == null) return;
                    SimpleSlash simple = BotSlash[SelectIndex];
                    SlashCommand? slash = await LoadSingleCommandAsync(simple);
                    NowSlash = slash;
                    pathIndex.Clear();
                }
                else {
                    SlashCommand slash = LocalCache[SelectIndex];
                    NowSlash = slash;
                    pathIndex.Clear();
                    UpdateCommand(slash);
                }

            }
            else {
                NowSlash = null;
                plusbutton1.Enabled = false;
                minusbutton1.Enabled = false;
                groupBox3.Visible = false;
                groupBox6.Visible = false;
            }
            groupBox4.Enabled = true;
            label1.Visible = true;
            Operation = true;
            if (Status) label1.Text = Resource.ToLocal;
            else label1.Text = Resource.ToBot;
        }
        private void listBox3_DoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.listBox3.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches) {
                EnterOption(NowFloor + 1);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1) {
                EnterOption(NowFloor + 1);
            }
        }
        private void EnterOption(int enter)
        {
            groupBox5.Controls.Clear();
            LinkLabel label = new LinkLabel();
            label.AutoSize = true;
            if (NowSlash == null) return;
            label.Text = $"/{NowSlash.name}";
            label.Location = new Point(3, 10);
            label.Font = new Font("Arial", 12);
            label.Tag = 0;
            label.Click += Link_Click;
            groupBox5.Controls.Add(label);
            int x = 3 + label.Width;
            Operation = false;
            if (NowFloor >= enter) {
                NowFloor = enter;
                VisitPath(x);
                if (enter == 0) {
                    UpdateCommand(NowSlash);
                    pathIndex.Clear();
                    NowOption = null;
                }
                Operation = true;
                return;
            }
            NowFloor++;
            int[] path = pathIndex.ToArray();
            int index = listBox3.SelectedIndex;
            checkBox3.Visible = false;
            if (NowOption != null) {
                if (NowOption.type != 1 && NowOption.type != 2) {
                    checkBox1.Visible = true;
                    checkBox2.Visible = true;
                    editchoiceButton.Visible = true;
                    groupBox6.Visible = true;
                }
            }
            for (int i = 0; i < NowFloor; i++) {
                if (NowOption == null) {
                    if (NowSlash.options == null) return;
                    CommandOption option = NowSlash.options[index];
                    pathIndex.Add(listBox3.SelectedIndex);
                    NowOption = option;
                    x += UpdateOption(option, x, i);
                }
                else {
                    if (NowOption.options != null) {
                        if (NowFloor != i + 1 || index == -1) {
                            if (i == 0) NowOption = NowSlash.options[path[i]];
                            else NowOption = NowOption.options[path[i]];
                        }
                        else {
                            pathIndex.Add(index);
                            NowOption = NowOption.options[index];
                        }
                    }
                    x += UpdateOption(NowOption, x, i);
                }
            }
            backbutton.Enabled = true;
            Operation = true;
        }
        private async Task<bool> ChangeEvent(ListBox listBox)
        {
            if (!Changed) return true;
            DialogResult dialog = DialogResult.Yes;
            if (AutoSync == false) dialog = MessageBox.Show(Resource.ChangeMsg, Resource.Warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes || dialog == DialogResult.No) {
                if (BotSlash == null) return false;
                if (NowSlash != null) {
                    if (dialog == DialogResult.Yes) {
                        if (Status) await SaveSingleCommand(NowSlash);
                        else {
                            if (!local.SaveFile()) return false;
                            if (!local.WriteFile(LocalCache.ToArray())) {
                                MessageBox.Show(Resource.SaveFailLocal, Resource.FileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            SlashCommand[]? slashes = local.LoadFile();
                            if (slashes != null) LocalCache = slashes.ToList();
                        }
                    }
                    if (Status) BotCache.Remove(NowSlash);
                }
                Changed = false;
                return true;
            }
            else {
                Operation = false;
                if (Status) {
                    listBox1.ClearSelected();
                    listBox1.SelectedIndex = SelectIndex;
                }
                else {
                    listBox2.ClearSelected();
                    listBox2.SelectedIndex = SelectIndex;
                }
                Operation = true;
                return false;
            }
        }
        private void BackOption_Click(object sender, EventArgs e)
        {
            EnterOption(NowFloor - 1);
        }
        private void VisitPath(int x)
        {
            int[] path = pathIndex.ToArray();
            if (NowSlash == null || NowSlash.options == null) return;
            for (int i = 0; i < NowFloor; i++) {
                if (i == 0) {
                    NowOption = NowSlash.options[path[0]];
                }
                else {
                    NowOption = NowOption.options[path[i]];
                }
                x += UpdateOption(NowOption, x, i);
            }
            backbutton.Enabled = true;
        }
        private List<CommandOption> GetPreviousOptions()
        {
            int[] path = pathIndex.ToArray();
            List<CommandOption> options = new List<CommandOption>();
            if (NowFloor == 0) return options;
            if (NowSlash == null || NowSlash.options == null) return options;
            options = NowSlash.options;
            for (int i = 1; i < NowFloor; i++) {
                options = options[path[i - 1]].options;
            }
            return options;
        }

        private int UpdateOption(CommandOption option, int x, int index)
        {
            LinkLabel label = new LinkLabel();
            label.AutoSize = true;
            label.Text = $"/{option.name}";
            label.Location = new Point(x, 10);
            label.Font = new Font("Arial", 12);
            label.Tag = index + 1;
            label.Click += Link_Click;
            groupBox5.Controls.Add(label);
            listBox3.Items.Clear();
            if (option.options != null && option.options.Count > 0) {
                for (int i = 0; i < option.options.Count; i++) {
                    listBox3.Items.Add($"[{option.options[i].type}] {option.options[i].name}");
                }
            }
            cmdnameTextbox.Text = option.name;
            cmdnameTextbox.ReadOnly = false;
            cmddescTextbox.Text = option.description;
            if (option.type > 2) addoptionButton.Enabled = false;
            else addoptionButton.Enabled = true;
            UpdateParameter(option);
            if (NowSlash != null) UpdateType(NowSlash, option.type, false);
            return label.Width;
        }
        private void UpdateParameter(CommandOption option)
        {
            if (option.required != null) checkBox1.Checked = (bool)option.required;
            if (option.autocomplete != null) checkBox2.Checked = (bool)option.autocomplete;
            checkBox1.Visible = true;
            if (option.type == 1 || option.type == 2) checkBox1.Visible = false;
            checkBox2.Visible = false;
            editchoiceButton.Visible = false;
            CommandType? type = types.FirstOrDefault(m => m.value == option.type);
            if (type != null && type.choice) {
                checkBox2.Visible = true;
                checkBox2.Enabled = true;
                editchoiceButton.Enabled = true;
                editchoiceButton.Visible = true;
                if (option.choices != null) checkBox2.Enabled = false;
                else if (checkBox2.Checked) editchoiceButton.Enabled = false;
            }
        }

        private void Link_Click(object? sender, EventArgs e)
        {
            LinkLabel? label = sender as LinkLabel;
            if (label == null || label.Tag == null) return;
            EnterOption((int)label.Tag);
        }

        public static string? LocalizationType;
        private void localization_Click(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            LocalizationType = (string)button.Tag;
            LocalizationForm form = new LocalizationForm();
            form.StartPosition = FormStartPosition.CenterParent;
            if (NowSlash == null) return;
            SlashCommand? CACHE = SlashCommand.Clone(NowSlash);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                Changed = true;
            else
                NowSlash = CACHE;
        }
        private void editOptionButton(object sender, EventArgs e)
        {
            EditOptionForm form = new EditOptionForm();
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK) {
                Changed = true;
                Operation = false;
                if (NowSlash != null) UpdateCommand(NowSlash);
                VisitPath(NowFloor);
                Operation = true;
            }
        }
        private void editchoiceButton_Click(object sender, EventArgs e)
        {
            ChoiceForm form = new ChoiceForm();
            List<CommandOptionChoice> CACHE = [];
            if (NowOption == null) return;
            if (NowOption.choices != null) CACHE = new List<CommandOptionChoice>(NowOption.choices);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK) {
                Changed = true;
                Operation = false;
                Operation = true;
            }
            else
                NowOption.choices = CACHE;
            if (NowOption.choices != null) {
                if (NowOption.choices.Count <= 0) NowOption.choices = null;
            }
            UpdateParameter(NowOption);
        }
        private void checkBox_Click(object sender, EventArgs e)
        {
            if (!Operation) return;
            if (NowSlash == null) return;
            CheckBox? checkBox = sender as CheckBox;
            if (checkBox == null || checkBox.Tag == null) return;
            if ((string)checkBox.Tag == "nsfw") {
                NowSlash.nsfw = checkBox.Checked;
                Changed = true;
            }
            if (NowOption == null) return;
            if ((string)checkBox.Tag == "required") {
                NowOption.required = checkBox.Checked;
                Changed = true;
            }
            else if ((string)checkBox.Tag == "autocomplete") {
                NowOption.autocomplete = checkBox.Checked;
                editchoiceButton.Enabled = !checkBox.Checked; //自動化與choice不能共存
                Changed = true;
            }
        }
        private void plusbutton_Click(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            plusButton_Event((string)button.Tag);
        }
        private async void plusButton_Event(string tag)
        {
            if (tag == "Bot") {
                if (string.IsNullOrWhiteSpace(textBox1.Text)) {
                    MessageBox.Show(Resource.NameEmpty, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (BotSlash == null) return;
                if (BotSlash.SingleOrDefault(item => item.name == textBox1.Text) != null) {
                    MessageBox.Show(Resource.NameExist, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!await ChangeEvent(listBox1)) return;
                Status = true;
                SlashCommand slash = new SlashCommand();
                slash.name = textBox1.Text;
                slash.description = "-";
                slash.type = 1;
                if (select == null) return;
                await discord.PostCommandAsync(select, slash, toolStripProgressBar1);
                await LoadCommands();
                textBox1.Text = string.Empty;
            }
            else if (tag == "Local") {
                if (string.IsNullOrWhiteSpace(textBox2.Text)) {
                    MessageBox.Show(Resource.NameEmpty, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (LocalCache.SingleOrDefault(item => item.name == textBox2.Text) != null) {
                    MessageBox.Show(Resource.NameExist, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!await ChangeEvent(listBox2)) return;
                Status = false;
                SlashCommand slash = new SlashCommand();
                slash.name = textBox2.Text;
                slash.description = "-";
                slash.type = 1;
                LocalCache.Add(slash);
                LoadLocalCommands();
                textBox2.Text = string.Empty;
                IsSave = false;
            }
        }
        private async void minusbutton_ClickAsync(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            if (NowSlash == null) return;
            if ((string)button.Tag == "Bot" && Status) {
                if (select == null) return;
                if (NowSlash.id == null) return;
                await discord.DeleteCommandAsync(select, NowSlash.id, toolStripProgressBar1);
                BotCache.Remove(NowSlash);
                await LoadCommands();
                NowSlash = null;
            }
            else if ((string)button.Tag == "Local" && !Status) {
                LocalCache.Remove(NowSlash);
                LoadLocalCommands();
                IsSave = false;
            }
        }
        private async void reloadButton_ClickAsync(object sender, EventArgs e)
        {
            if (NowSlash == null) return;
            LoadClients();
            await VerityValidClientAsync();
            await LoadCommands();
            listBox1.ClearSelected();
            listBox2.ClearSelected();
            listBox3.ClearSelected();
            BotCache.Clear();
            SelectIndex = -1;
            label1.Visible = false;
            groupBox4.Enabled = false;
            groupBox3.Visible = false;
        }
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S && Changed) {
                SaveCommand();
            }
            else if (e.KeyCode == Keys.Enter) {
                if (!string.IsNullOrEmpty(textBox1.Text)) plusButton_Event("Bot");
                else if (!string.IsNullOrEmpty(textBox2.Text)) plusButton_Event("Local");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            SaveCommand();
        }
        private async void SaveCommand()
        {
            double time = discord.IsRequestAllowed(3000);
            if (time != -1) {
                toolStripStatusLabel1.Text = $"{Resource.SlowDown1}{string.Format("{0:F2}", time)}{Resource.SlowDown2}";
                return;
            }
            if (Status) {
                if (BotSlash == null) return;
                SimpleSlash simple = BotSlash[SelectIndex];
                if (NowSlash == null) return;
                bool success = await SaveSingleCommand(NowSlash);
                if (!success) {
                    toolStripStatusLabel1.Text = Resource.SaveFail;
                    MessageBox.Show(Resource.SaveFail, Resource.NetworkError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                BotCache.Remove(NowSlash);
                await LoadSingleCommandAsync(simple);
            }
            else {
                LocalSave(false);
            }
            toolStripStatusLabel1.Text = Resource.Saved;
            Changed = false;
        }
        string? lastname;
        string? lastdesc;
        private void TextChangedEvent(object sender, EventArgs e)
        {
            TextBox? textBox = sender as TextBox;
            if (textBox == null || textBox.Tag == null) return;
            if (NowSlash == null) return;
            if (!Operation) return;
            string tag = (string)textBox.Tag;
            string txt = textBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(txt)) {
                MessageBox.Show(Resource.NameEmpty, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tag == "name") {
                bool isValid = Regex.IsMatch(txt, @"^[a-z0-9_-]+$");
                if (!isValid) {
                    MessageBox.Show(Resource.Nameillegal, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Text = lastname;
                    textBox.SelectionStart = textBox.Text.Length;
                    return;
                }
                if (txt.Length > 32) {
                    MessageBox.Show($"{Resource.WordLimit1} 1~32 {Resource.WordLimit2}", Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Text = lastname;
                    textBox.SelectionStart = textBox.Text.Length;
                    return;
                }
                lastname = txt;
            }
            if (tag == "description") {
                if (txt.Length > 100) {
                    MessageBox.Show($"{Resource.WordLimit1} 1~100 {Resource.WordLimit2}", Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox.Text = lastdesc;
                    textBox.SelectionStart = textBox.Text.Length;
                    return;
                }
                lastdesc = txt;
            }
            if (NowOption != null && NowFloor > 0) {
                if (tag == "name") NowOption.name = txt;
                if (tag == "description") NowOption.description = txt;
                Changed = true;
            }
            else {
                if (tag == "name") NowSlash.name = textBox.Text;
                if (tag == "description") NowSlash.description = textBox.Text;
                Changed = true;
            }
        }
        private void TypeChangedEvent(object sender, EventArgs e)
        {
            if (!Operation) return;
            if (NowSlash == null) return;
            if (comboBox1.SelectedIndex == -1) return;
            int type = tps[comboBox1.SelectedIndex].value;
            if (NowOption == null) NowSlash.type = type;
            else {
                NowOption.type = type;
                Operation = false;
                UpdateParameter(NowOption);
                Operation = true;
            }
            Changed = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (BotSlash == null) return;
            if (BotSlash.Length == 0) return;
            int worknumber = 0;
            BotCache.Clear();
            if (Status) {
                if (listBox1.SelectedIndices.Count < 0) return;
                toolStripStatusLabel1.Text = $"{Resource.LoadingCommands} (0/{listBox1.SelectedIndices.Count})";
                for (int i = 0; i < listBox1.SelectedIndices.Count; i++) {
                    int index = listBox1.SelectedIndices[i];
                    string? id = BotSlash[index].id;
                    if (id == null) return;
                    backupEvent(id);
                    worknumber++;
                    toolStripStatusLabel1.Text = $"{Resource.LoadingCommands} ({worknumber}/{listBox1.SelectedIndices.Count})";
                }
                toolStripStatusLabel1.Text = $"{Resource.LoadingCommands} {Resource.LoadingComplete}";
                toolStripProgressBar1.Value = 100;
                IsSave = false;
            }
            else {
                await LoadCommands();
                if (listBox2.SelectedIndices.Count < 0) return;
                toolStripStatusLabel1.Text = $"{Resource.PushingCommands} (0/{listBox2.SelectedIndices.Count})";
                for (int i = 0; i < listBox2.SelectedIndices.Count; i++) {
                    int index = listBox2.SelectedIndices[i];
                    PushEvent(index);
                    worknumber++;
                    toolStripStatusLabel1.Text = $"{Resource.PushingCommands} ({worknumber}/{listBox2.SelectedIndices.Count})";
                }
                toolStripStatusLabel1.Text = $"{Resource.PushingCommands} {Resource.LoadingComplete}";
                toolStripProgressBar1.Value = 100;
            }
            await LoadCommands();
            LoadLocalCommands();
        }

        private async void backupToLocal_Click(object sender, EventArgs e)
        {
            await LoadCommands();
            if (BotSlash == null) return;
            if (BotSlash.Length == 0) return;
            if (LocalCache.Count > 0) {
                DialogResult result = MessageBox.Show(Resource.BackupNotEmpty, Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel) return;
            }
            int worknumber = 0;
            BotCache.Clear();

            toolStripStatusLabel1.Text = $"{Resource.LoadingCommands} (0/{BotSlash.Length})";
            for (int i = 0; i < BotSlash.Length; i++) {
                string? id = BotSlash[i].id;
                if (id == null) return;
                backupEvent(id);
                worknumber++;
                toolStripStatusLabel1.Text = $"{Resource.LoadingCommands} ({worknumber}/{BotSlash.Length})";
            }
            toolStripStatusLabel1.Text = $"{Resource.LoadingCommands} {Resource.LoadingComplete}";
            toolStripProgressBar1.Value = 100;
            IsSave = false;
        }
        private async void backupEvent(string id)
        {
            if (select == null) return;
            SlashCommand? slash = await discord.GetCommandAsync(select, id, toolStripProgressBar1);
            if (slash == null) return;
            SlashCommand? copy = SlashCommand.Clone(slash);
            if (copy == null) return;
            int index = LocalCache.FindIndex(m => m.name == slash.name);
            if (index != -1) {
                DialogResult dialog = MessageBox.Show($"{Resource.PushDuplicate1}{slash.name}{Resource.PushDuplicate2}", Resource.Warning, MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No) return;
                LocalCache[index] = copy;
            }
            else
                LocalCache.Add(copy);
            BotCache.Add(slash);
            //update toollabel
            LoadLocalCommands();
            await Task.Delay(new TimeSpan(0, 0, 0, 0, 120));
        }

        private async void PushToBot_Click(object sender, EventArgs e)
        {
            if (BotSlash == null) return;
            if (BotSlash.Length == 0) return;
            int worknumber = 0;
            await LoadCommands();
            BotCache.Clear();
            toolStripStatusLabel1.Text = $"{Resource.PushingCommands} (0/{LocalCache.Count})";
            for (int i = 0; i < LocalCache.Count; i++) {
                PushEvent(i);
                worknumber++;
                toolStripStatusLabel1.Text = $"{Resource.PushingCommands} ({worknumber}/{LocalCache.Count})";
            }
            toolStripStatusLabel1.Text = $"{Resource.PushingCommands} {Resource.LoadingComplete}";
            toolStripProgressBar1.Value = 100;
        }
        private async void PushEvent(int i)
        {
            if (select == null) return;
            if (BotSlash == null) return;
            SlashCommand slash = LocalCache[i];
            if (BotSlash.ToList().FindIndex(m => m.name == slash.name) != -1) {
                DialogResult dialog = MessageBox.Show($"{Resource.PushDuplicate1}{slash.name}{Resource.PushDuplicate2}", Resource.Warning, MessageBoxButtons.YesNo);
                if (dialog == DialogResult.No) return;
            }
            await discord.PostCommandAsync(select, slash, toolStripProgressBar1);
            await Task.Delay(new TimeSpan(0, 0, 0, 0, 100));
        }

        private async void LoadFile_Click(object sender, EventArgs e)
        {
            if (!await ChangeEvent(listBox2)) return;
            Changed = false;
            if (!local.OpenFile()) return;
            SlashCommand[]? slashes = local.LoadFile();
            if (slashes == null) {
                MessageBox.Show(Resource.LoadFileError, Resource.FileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (slashes.Length <= 0) {
                MessageBox.Show(Resource.LoadFileEmpty, Resource.FileError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LocalCache = slashes.ToList();
            LoadLocalCommands();
            groupBox2.Text = $"{Resource.LoadFrom} {LocalFileHelper.FileName}";
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            LocalSave(true);
        }
        private void LocalSave(bool hint)
        {
            if (Status && hint) {
                MessageBox.Show(Resource.SaveLocalUnavailable, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrEmpty(LocalFileHelper.FilePath) && hint && !AutoSync) {
                DialogResult result = MessageBox.Show(Resource.SaveOverwrite, Resource.Warning, MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes || result == DialogResult.No) {
                    if (result == DialogResult.No)
                        if (!local.SaveFile()) return;
                    if (!local.WriteFile(LocalCache.ToArray())) {
                        MessageBox.Show(Resource.SaveFailLocal, Resource.FileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else {
                if (string.IsNullOrEmpty(LocalFileHelper.FilePath))
                    if (!local.SaveFile()) return;
                if (!local.WriteFile(LocalCache.ToArray())) {
                    MessageBox.Show(Resource.SaveFailLocal, Resource.FileError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            SlashCommand[]? slashes = local.LoadFile();
            if (slashes != null) LocalCache = slashes.ToList();
            LoadLocalCommands();
            toolStripStatusLabel1.Text = Resource.Saved;
            IsSave = true;
        }
        bool close = false;
        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close) {
                close = false;
                return;
            }
            if (Changed || !IsSave) {
                DialogResult dialog = DialogResult.Yes;
                if (!AutoSync) dialog = MessageBox.Show(Resource.ChangeMsg, Resource.Warning, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes || dialog == DialogResult.No) {
                    if (dialog == DialogResult.Yes) {
                        if (Status && NowSlash != null) await SaveSingleCommand(NowSlash);
                        Status = false;
                        if (LocalCache.Count > 0) LocalSave(false);
                    }
                    close = true;
                }
                else {
                    close = true;
                    e.Cancel = true;
                }
            }
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(LocalFileHelper.fileFolder)) {
                Process.Start("explorer.exe", LocalFileHelper.fileFolder);
            }
        }

        private void ResetDatabase(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(Resource.DeleDatabase, Resource.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes) {
                sql.DeleteDatabase();
                if (sql.CreateDatabase()) {
                    sql.CreateTable();
                    sql.CreateData();
                }
                LoadClients();
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                groupBox3.Visible = false;
                groupBox4.Enabled = false;
                MessageBox.Show(Resource.ResetDone, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ClearLocalAndExit(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(Resource.DeletaLocalFile, Resource.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes) {
                DialogResult dialog2 = MessageBox.Show(Resource.DeleteLocalFile2, Resource.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog2 == DialogResult.Yes) {
                    sql.DeleteDatabase();
                    sql.DeleteLocalFolder();
                    Application.Exit();
                }
            }
        }
        private void about_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog();
        }

        public static CommandType[] types = new CommandType[]
        {
            new CommandType("SUB_COMMAND", 1, true, false),
            new CommandType("SUB_COMMAND_GROUP", 2, true, false),
            new CommandType("STRING", 3, false, true),
            new CommandType("INTEGER", 4, false, true),
            new CommandType("BOOLEAN", 5, false, false),
            new CommandType("USER", 6, false, false),
            new CommandType("CHANNEL", 7, false, false),
            new CommandType("ROLE", 8, false, false),
            new CommandType("MENTIONABLE", 9, false, false),
            new CommandType("NUMBER (Double)", 10, false, true),
            new CommandType("ATTACHMENT", 11, false, false)
        };
        public class Client
        {
            public int id { get; set; }
            public string Name { get; set; }
            public string ApplicationID { get; set; }
            public string ClientToken { get; set; }
            public Client(int id, string Name, string ApplicationID, string ClientToken)
            {
                this.id = id;
                this.Name = Name;
                this.ApplicationID = ApplicationID;
                this.ClientToken = ClientToken;
            }
        }
    }
}
