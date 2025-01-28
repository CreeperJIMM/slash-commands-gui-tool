using System;
using DiscordAPI;
using ThemeSW;

namespace slash_commands_gui_tool
{
    public partial class Form2 : Form
    {
        public static DiscordUser? User = new DiscordUser();
        public static string ClientToken = "";
        public static bool Saved = true;

        public Form2()
        {
            ThemeHelper theme = new ThemeHelper();
            theme.SetTheme(this);
            Form1.ChangeLanguage(Form1.USER_LANGUAGE);
            InitializeComponent(); 
        }
        private DiscordAPIhelper discord = new DiscordAPIhelper();
        private async void Sumbit_Click(object sender, EventArgs e)
        {
            string ApplicationID = textBox1.Text;
            ClientToken = textBox2.Text;
            Saved = checkBox1.Checked;
            //User = new DiscordUser();
            //User.Username = "hello";
            //User.Id = ApplicationID;
            if (string.IsNullOrWhiteSpace(ApplicationID) || string.IsNullOrWhiteSpace(ClientToken)) {
                MessageBox.Show(Resource.Message, Resource.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form1.Client client = new Form1.Client(0, "", ApplicationID, ClientToken);
            User = await discord.GetClientAsync(client);
            if (User == null) {
                MessageBox.Show(Resource.ClientErrorMsg, Resource.ClientError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (User.Id != ApplicationID) {
                MessageBox.Show(Resource.ClientErrorMsg2, Resource.ClientError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
