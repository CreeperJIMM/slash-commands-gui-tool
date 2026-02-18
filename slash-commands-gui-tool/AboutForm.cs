using System.Diagnostics;
using ThemeSW;

namespace slash_commands_gui_tool
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            ThemeHelper theme = new ThemeHelper();
            theme.SetTheme(this);
            Form1.ChangeLanguage(Form1.USER_LANGUAGE);
            InitializeComponent();
        }

        static private readonly string _Version = "1.3";
        private void AboutForm_Load(object sender, EventArgs e)
        {
            if (Environment.Is64BitProcess) {
                label5.Text += " 64 bit";
            }
            else {
                label5.Text += " 32 bit";
            }
            label5.Text = label5.Text.Replace("X", _Version);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void link_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel? linkLabel = sender as LinkLabel;
            if (linkLabel == null || linkLabel.Tag == null) return;
            string url = (string)linkLabel.Tag;
            OpenURL(url);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button? button = sender as Button;
            if (button == null || button.Tag == null) return;
            string url = (string)button.Tag;
            OpenURL(url);
        }

        private void OpenURL(string url)
        {
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
    }
}
