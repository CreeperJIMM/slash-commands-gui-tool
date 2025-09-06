using DiscordAPI;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using ThemeSW;

namespace slash_commands_gui_tool
{
    public partial class EditOptionForm : Form
    {
        public EditOptionForm()
        {
            ThemeHelper theme = new ThemeHelper();
            theme.SetTheme(this);
            Form1.ChangeLanguage(Form1.USER_LANGUAGE);
            InitializeComponent();
        }
        public bool Changed = false;
        List<CommandOption>? options = new List<CommandOption>();
        int TYPE = -1;
        private void EditOptionForm_Load(object sender, EventArgs e)
        {
            if (Form1.NowOption == null) {
                if (Form1.NowSlash == null) return;
                if(Form1.NowSlash.options == null) Form1.NowSlash.options = new List<CommandOption>();
                options = Form1.NowSlash.options;
                TYPE = Form1.NowSlash.type;
            }
            else {
                Form1.NowOption.options ??= new List<CommandOption>();
                options = Form1.NowOption.options;
                TYPE = Form1.NowOption.type;
            }
            if (options == null) return;
            UpdateListbox();
        }
        List<CommandType> types = new List<CommandType>(Form1.types);
        private void UpdateListbox()
        {
            listBox1.Items.Clear();
            if (options == null) return;
            foreach (CommandOption option in options) {
                listBox1.Items.Add($"[{option.type}] {option.name}");
            }
            bool sub = IsHasSubCommnad();
            bool param = IsHasParameter();
            types = new List<CommandType>(Form1.types);
            comboBox1.Items.Clear();
            if (sub || TYPE == 2) { //若有子指令則刪除參數選項
                types.RemoveRange(2, types.Count - 2);
            }
            if (param) { //若有參數則刪除子指令的選項
                types.RemoveRange(0, 2);
            }
            if (Form1.NowFloor != 0 && !param && sub || TYPE == 2) { //若不為最頂層且沒有參數則刪除Group Command
                types.Remove(Form1.types[1]);
            }
            foreach (CommandType type in types) {
                comboBox1.Items.Add($"[{type.value}] {type.name}");
            }
            comboBox1.SelectedIndex = 0;
        }
        private void deleteOption_click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                options?.RemoveAt(listBox1.SelectedIndex);
                UpdateListbox();
                Changed = true;
            }
        }

        private void addOption_click(object sender, EventArgs e)
        {
            addNewOption(false);
        }
        private void addNewOption(bool IsKey)
        {
            if (comboBox1.SelectedIndex == -1) return;
            if (string.IsNullOrWhiteSpace(textBox1.Text)) {
                if (!IsKey) MessageBox.Show(Resource.NameEmpty, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isValid = Regex.IsMatch(textBox1.Text, @"^[a-z0-9_-]+$");
            if (!isValid) {
                if(!IsKey) MessageBox.Show(Resource.Nameillegal, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string txt = Regex.Replace(textBox1.Text, @"\s+", "");
            CommandOption? same = null;
            if (options != null) same = options.Find(m => m.name == txt);
            if (same != null) {
                if (!IsKey) MessageBox.Show(Resource.NameExist, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommandOption option = new CommandOption(types[comboBox1.SelectedIndex].value, txt, "-");
            if (options == null) return;
            options.Add(option);
            textBox1.Text = string.Empty;
            UpdateListbox();
            Changed = true;
        }
        private void EditOptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Changed) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }

        private bool IsHasSubCommnad()
        {
            bool flag = false;
            if (options == null) return false;
            foreach (CommandOption option in options) {
                if (option.type == 1 || option.type == 2) flag = true;
            }
            return flag;
        }
        private bool IsHasParameter()
        {
            bool flag = false;
            if (options == null) return false;
            foreach (CommandOption option in options) {
                if (option.type > 2) flag = true;
            }
            //檢測是否符合規則(如果當前type為1且不為頂層一律輸出true)
            if (Form1.NowFloor >= 1 && TYPE == 1) {
                return true;
            }
            return flag;
        }

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                addNewOption(true);
            }else if (e.KeyCode == Keys.Delete) {
                if (listBox1.SelectedIndex != -1) {
                    options?.RemoveAt(listBox1.SelectedIndex);
                    UpdateListbox();
                    Changed = true;
                }
            }
        }
    }
}
