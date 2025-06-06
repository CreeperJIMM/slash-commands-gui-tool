using DiscordAPI;
using System.Text.RegularExpressions;
using ThemeSW;

namespace slash_commands_gui_tool
{
    public partial class LocalizationForm : Form
    {
        public LocalizationForm()
        {
            ThemeHelper theme = new ThemeHelper();
            theme.SetTheme(this);
            Form1.ChangeLanguage(Form1.USER_LANGUAGE);
            InitializeComponent();
            scale = this.DeviceDpi / 96f;
            this.AutoScroll = true;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
        }
        private List<GP> groups = new List<GP>();
        public Dictionary<string, string> localization = new Dictionary<string, string>();
        public bool Changed = false;
        bool Operation = false;
        float scale = 100f;
        string? type;
        List<Language> nowlang = new List<Language>();
        private void LocalizationForm_Load(object sender, EventArgs e)
        {
            localization = new Dictionary<string, string>();
            if (ChoiceForm.LocalizationEdit != null &&  ChoiceForm.LocalizationEdit.StartsWith("Choice,")) {
                string name = ChoiceForm.LocalizationEdit.Split(',')[1];
                List<CommandOptionChoice> choices = ChoiceForm.choices;
                CommandOptionChoice? choice = choices.Find(m => m.name == name);
                if (choice == null) {
                    Close();
                    return;
                }
                if (choice.name_localizations == null) choice.name_localizations = new Dictionary<string, string>();
                localization = choice.name_localizations;
                this.Text = $"{Resource.Name} - {Resource.LocalizationConfig}";
                UpdateData(e, false);
                return;
            }
            CommandOption? option = Form1.NowOption;
            SlashCommand? slash = Form1.NowSlash;
            if (slash == null) {
                Close();
                return;
            }
            type = Form1.LocalizationType;
            if (type == null) {
                Close();
                return;
            }
            if (type == "name") {
                if (option != null) {
                    if (option.name_localizations == null) option.name_localizations = new Dictionary<string, string>();
                    localization = option.name_localizations;
                }
                else {
                    if (slash.name_localizations == null) slash.name_localizations = new Dictionary<string, string>();
                    localization = slash.name_localizations;
                }
                this.Text = $"{Resource.Name} - {Resource.LocalizationConfig}";
            }
            else if (type == "description") {
                if (option != null) {
                    if (option.description_localizations == null) option.description_localizations = new Dictionary<string, string>();
                    localization = option.description_localizations;
                }
                else {
                    if (slash.description_localizations == null) slash.description_localizations = new Dictionary<string, string>();
                    localization = slash.description_localizations;
                }
                this.Text = $"{Resource.Desc} - {Resource.LocalizationConfig}";
            }
            UpdateData(e, false);
        }

        private void Minus_Click(object? sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            string locale = (string)button.Tag;
            localization.Remove(locale);
            UpdateData(e, false);
            Changed = true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if(nowlang.Count <= 0) return;
            int index = comboBox1.SelectedIndex;
            Language lang = nowlang[index];
            if (lang.locale == null) return;
            localization.Add(lang.locale, "");
            UpdateData(e, true);
            Changed = true;
        }

        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if(Operation) Changed = true;
        }
        //關閉視窗檢查
        private void LocalizationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GP[] gps = groups.ToArray();
            for (int i = 0; i < gps.Length; i++) {
                GP gp = gps[i];
                Button? button = gp.Button;
                if (button.Tag == null) return;
                string locale = (string)button.Tag;
                string txt = gp.TextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(txt)) {
                    DialogResult dialog = MessageBox.Show($"{Resource.NameEmpty}\n\nError: {locale}", Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK) {
                        e.Cancel = true;
                        return;
                    }
                    else Changed = false;
                }
                if (type == "name") {
                    bool isValid = Regex.IsMatch(txt, @"^[a-zA-Z0-9_\p{L}-]+$");
                    if (!isValid) {
                        DialogResult dialog = MessageBox.Show($"{Resource.Nameillegal2}\n\nError: {locale}", Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.OK) {
                            e.Cancel = true;
                            return;
                        }
                        else Changed = false;
                    }
                    if (txt.Length > 32) {
                        DialogResult dialog = MessageBox.Show($"{Resource.WordLimit1} 1~32 {Resource.WordLimit2}\n\nError: {locale}", Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.OK) {
                            e.Cancel = true;
                            return;
                        }
                        else Changed = false;
                    }
                }
                if (type == "description") {
                    if (txt.Length > 100) {
                        DialogResult dialog = MessageBox.Show($"{Resource.WordLimit1} 1~100 {Resource.WordLimit2}\n\nError: {locale}", Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (dialog == DialogResult.OK) {
                            e.Cancel = true;
                            return;
                        }
                        else Changed = false;
                    }
                }
                localization[locale] = txt;
            }
            if (Changed) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }

        private void UpdateData(EventArgs e, bool up)
        {
            Operation = false;
            GP[] gps = groups.ToArray();
            foreach (GP gp in gps) {
                this.Controls.Remove(gp.Label);
                this.Controls.Remove(gp.Button);
                this.Controls.Remove(gp.TextBox);
            }
            groups.Clear();
            int index = 0;
            if (localization.Count > 0) {
                foreach (KeyValuePair<string, string> pair in localization) {
                    Language? lang = language.FirstOrDefault(l => l.locale == pair.Key);
                    if (lang == null) continue;
                    GP gp = DrawComponent(lang, index, pair.Value);
                    groups.Add(gp);
                    index++;
                }
            }
            int offset = AutoScrollPosition.Y;
            
            comboBox1.Location = new Point(comboBox1.Location.X, offset + 30 + (index * 120));
            button1.Location = new Point(button1.Location.X, offset + 12 + (index * 120));
            if (!FormHelper.UpHasSpace(this, e, up, 120))
                if (!FormHelper.IsReachBottom(this, e, 120))
                    this.Size = new Size(this.Width, (index + 1) * 120);
                else
                    AutoScrollPosition = new Point(0, VerticalScroll.Maximum);
            else
                this.Size = new Size(this.Width, (index + 1) * 120);
            UpdateComboBox();
            Operation = true;
        }
        private GP DrawComponent(Language lang, int index, string txt)
        {
            int offset = AutoScrollPosition.Y;
            Label label = new Label();
            label.Text = $"{lang.locale} - {lang.name} ({lang.display})";
            label.AutoSize = true;
            label.Margin = new Padding(0);
            label.Font = new Font("Microsoft JhengHei UI", 10);
            label.Location = new Point(10, offset + 10 + (120 * index));

            RichTextBox textBox = new RichTextBox();
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.Size = new Size(this.Size.Width - (50 * (int)scale), 80);
            textBox.Text = txt;
            textBox.Margin = new Padding(0);
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Location = new Point(12, offset + 30 + (120 * index));

            Button button = new Button();
            button.BackColor = Color.FromArgb(255, 128, 128);
            button.FlatStyle = FlatStyle.Flat;
            button.Text = "-";
            button.Size = new Size(25, 25);
            button.Location = new Point(this.Size.Width - (60 * (int)scale), offset + 5 + (120 * index));
            button.Margin = new Padding(0);
            button.Click += Minus_Click;
            button.Tag = lang.locale;
            this.Controls.Add(label);
            this.Controls.Add(textBox);
            this.Controls.Add(button);
            return new GP(label, textBox, button);
        }

        private void UpdateComboBox()
        {
            nowlang.Clear();
            comboBox1.Items.Clear();
            foreach (Language lang in language) {
                if (lang.locale == null) continue;
                if (localization.ContainsKey(lang.locale)) continue;
                nowlang.Add(lang);
                comboBox1.Items.Add($"{lang.name}({lang.display})");
            }
            if(comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            else comboBox1.Text = string.Empty;
        }

        private class GP
        {
            public Label Label;
            public RichTextBox TextBox;
            public Button Button;
            public GP(Label label, RichTextBox textBox, Button button)
            {
                Label = label;
                TextBox = textBox;
                Button = button;
            }
        }
        public Language[] language =
        [
            new Language("en-US", "English(US)", "English, US"),
            new Language("en-GB", "English(UK)", "English, UK"),
            new Language("es-ES", "Spanish", "Español"),
            new Language("es-419", "Spanish, LATAM", "Español, LATAM"),
            new Language("id", "Indonesian", "Bahasa Indonesia"),
            new Language("da", "Danish", "Dansk"),
            new Language("de", "German", "Deutsch"),
            new Language("fr", "French", "Français"),
            new Language("hr", "Croatian", "Hrvatski"),
            new Language("it", "Italian", "Italiano"),
            new Language("lt", "Lithuanian", "Lietuviškai"),
            new Language("hu", "Hungarian", "Magyar"),
            new Language("nl", "Dutch", "Nederlands"),
            new Language("no", "Norwegian", "Norsk"),
            new Language("pl", "Polish", "Polski"),
            new Language("pt-BR", "Portuguese(BR)", "Português do Brasil"),
            new Language("ro", "Romanian, Romania", "Română"),
            new Language("fi", "Finnish", "Suomi"),
            new Language("sv-SE", "Swedish", "Svenska"),
            new Language("vi", "Vietnamese", "Tiếng Việt"),
            new Language("tr", "Turkish", "Türkçe"),
            new Language("cs", "Czech", "Čeština"),
            new Language("el", "Greek", "Ελληνικά"),
            new Language("bg", "Bulgarian", "български"),
            new Language("ru", "Russian", "Pусский"),
            new Language("uk", "Ukrainian", "Українська"),
            new Language("hi", "Hindi", "हिन्दी"),
            new Language("th", "Thai", "ไทย"),
            new Language("ja", "Japanese", "日本語"),
            new Language("zh-CN", "Chinese(China)", "中文"),
            new Language("zh-TW", "Chinese(Taiwan)", "繁體中文"),
            new Language("ko", "Korean", "한국어")
        ];
    }
}
