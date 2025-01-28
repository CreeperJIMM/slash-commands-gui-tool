using DiscordAPI;
using System.Text.RegularExpressions;
using ThemeSW;

namespace slash_commands_gui_tool
{
    public partial class ChoiceForm : Form
    {
        public ChoiceForm()
        {
            ThemeHelper theme = new ThemeHelper();
            theme.SetTheme(this);
            Form1.ChangeLanguage(Form1.USER_LANGUAGE);
            InitializeComponent();
            this.AutoScroll = true;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
        }
        private List<GP> groups = new List<GP>();
        public static List<CommandOptionChoice> choices = new List<CommandOptionChoice>();
        public static string? LocalizationEdit = null;
        public bool Changed = false;
        bool Operation = false;
        int type = 3;
        private void ChoiceForm_Load(object sender, EventArgs e)
        {
            CommandOption? option = Form1.NowOption;
            if (option == null) {
                Close();
                return;
            }
            type = option.type;
            this.Text = $"{Resource.ChoiceConfig} - {Form1.types[type - 1].name}";
            if (option.choices == null) option.choices = new List<CommandOptionChoice>();
            choices = option.choices;
            UpdateData(e);
        }

        private void Minus_Click(object? sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            string name = (string)button.Tag;
            CommandOptionChoice? choice = choices.FirstOrDefault(c => c.name == name);
            if (choice == null) return;
            choices.Remove(choice);
            UpdateData(e);
            Changed = true;
        }

        private void Localbutton_Click(object? sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            string name = (string)button.Tag;
            LocalizationEdit = name;
            LocalizationForm form = new LocalizationForm();
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK) {
                Changed = true;
                UpdateData(e);
            }
            LocalizationEdit = null;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            string txt = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(txt)) {
                MessageBox.Show($"{Resource.NameEmpty}", Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt.Length > 100) {
                MessageBox.Show($"{Resource.WordLimit1} 1~32 {Resource.WordLimit2}", Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool isValid = Regex.IsMatch(txt, @"^[a-z0-9_-]+$");
            if (!isValid) {
                MessageBox.Show(Resource.Nameillegal, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommandOptionChoice choice = new CommandOptionChoice();
            choice.name = txt;
            switch(type) {
                case 3:
                    choice.value = "";
                    break;
                case 4:
                    choice.value = 0;
                    break;
                case 10:
                    choice.value = 0.0;
                    break;
            }
            textBox1.Text = string.Empty;
            choices.Add(choice);
            UpdateData(e);
            Changed = true;
        }
        
        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            if (!Operation) return;
            RichTextBox? textBox = sender as RichTextBox;
            if (textBox == null || textBox.Tag == null) return;
            CommandOptionChoice? choice = choices.FirstOrDefault(c => c.name == (string)textBox.Tag);
            if (choice == null) return;
            string txt = textBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(txt)) return;
            if(CheckText(txt, MessageBoxButtons.OK) != DialogResult.None) return;
            choice.value = txt;
            if (Operation) Changed = true;
        }

        //關閉視窗檢查
        private void ChoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GP[] gps = groups.ToArray();
            for (int i = 0; i < gps.Length; i++) {
                GP gp = gps[i];
                Button? button = gp.Button;
                if (button.Tag == null) return;
                string name = (string)button.Tag;
                string txt = gp.TextBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(txt)) {
                    DialogResult dialog = MessageBox.Show($"{Resource.NameEmpty}\n\nError: {name}", Resource.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dialog == DialogResult.OK) {
                        e.Cancel = true;
                        return;
                    }
                    else Changed = false;
                }
                else {
                    DialogResult result = CheckText(txt, MessageBoxButtons.OKCancel);
                    if (result != DialogResult.None) {
                        if(result == DialogResult.OK) {
                            e.Cancel = true;
                            return;
                        }else if(result == DialogResult.Cancel) {
                            Changed = false;
                        }
                    }
                }
            }
            if (Changed) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }
        private DialogResult CheckText(string txt, MessageBoxButtons btn)
        {
            if(txt.Length > 100) {
                return MessageBox.Show($"{Resource.WordLimit1}100{Resource.WordLimit2}", Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (type == 3) {
                bool isValid = Regex.IsMatch(txt, @"^[a-z0-9_-]+$");
                if (!isValid) {
                    return MessageBox.Show(Resource.Nameillegal, Resource.Warning, btn, MessageBoxIcon.Warning);
                }
            }
            else if (type == 4) {
                bool isValid = int.TryParse(txt, out _);
                if (!isValid) {
                    return MessageBox.Show(Resource.Nameillegal3, Resource.Warning, btn, MessageBoxIcon.Warning);
                }
            }
            else if (type == 10) {
                bool isValid = double.TryParse(txt, out _);
                if (!isValid) {
                    return MessageBox.Show(Resource.Nameillegal3, Resource.Warning, btn, MessageBoxIcon.Warning);
                }
            }
            return DialogResult.None;
        }
        private void UpdateData(EventArgs e)
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
            foreach (CommandOptionChoice choice in choices) {
                GP gp = DrawComponent(choice, index);
                groups.Add(gp);
                index++;
            }
            int offset = AutoScrollPosition.Y;
            label1.Location = new Point(23, offset + 10 + (index * 120));
            textBox1.Location = new Point(23, offset + 30 + (index * 120));
            button1.Location = new Point(285, offset + 12 + (index * 120));
            if (!FormHelper.UpHasSpace(this, e, 120))
                if (!FormHelper.IsReachBottom(this, e, 120))
                    this.Size = new Size(400, (index + 1) * 120);
                else
                    AutoScrollPosition = new Point(0, VerticalScroll.Maximum);
            else
                this.Size = new Size(400, (index + 1) * 120);
            Operation = true;
        }
        private GP DrawComponent(CommandOptionChoice choice, int index)
        {
            int offset = AutoScrollPosition.Y;
            Label label = new Label();
            label.Text = $"{choice.name}";
            label.AutoSize = true;
            label.Margin = new Padding(0);
            label.Font = new Font("Microsoft JhengHei UI", 10);
            label.Location = new Point(10, offset + 10 + (120 * index));

            RichTextBox textBox = new RichTextBox();
            textBox.BorderStyle = BorderStyle.Fixed3D;
            textBox.Size = new Size(340, 80);
            textBox.Text = $"{choice.value}";
            textBox.Margin = new Padding(0);
            textBox.Tag = choice.name;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Location = new Point(12, offset + 30 + (120 * index));

            Button button = new Button();
            button.BackColor = Color.FromArgb(255, 128, 128);
            button.FlatStyle = FlatStyle.Flat;
            button.Text = "-";
            button.Size = new Size(25, 25);
            button.Location = new Point(328, offset + 5 + (120 * index));
            button.Margin = new Padding(0);
            button.Click += Minus_Click;
            button.Tag = choice.name;

            Button Localbutton = new Button();
            Localbutton.BackColor = Color.FromArgb(128, 255, 128);
            Localbutton.FlatStyle = FlatStyle.Flat;
            Localbutton.Image = Images.localizations;
            Localbutton.Size = new Size(25, 25);
            Localbutton.Location = new Point(303, offset + 5 + (120 * index));
            Localbutton.Margin = new Padding(0);
            Localbutton.Click += Localbutton_Click;
            Localbutton.Tag = $"Choice,{choice.name}";
            this.Controls.Add(label);
            this.Controls.Add(textBox);
            this.Controls.Add(button);
            this.Controls.Add(Localbutton);
            return new GP(label, textBox, button, Localbutton);
        }

        private class GP
        {
            public Label Label;
            public RichTextBox TextBox;
            public Button Button;
            public Button Localbutton;
            public GP(Label label, RichTextBox textBox, Button button, Button localbutton)
            {
                Label = label;
                TextBox = textBox;
                Button = button;
                Localbutton = localbutton;
            }
        }
    }
}
