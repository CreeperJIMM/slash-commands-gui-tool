using Discord_Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThemeSW;

namespace slash_commands_gui_tool
{
    public partial class PermissionForm : Form
    {
        public PermissionForm()
        {
            ThemeHelper theme = new ThemeHelper();
            theme.SetTheme(this);
            Form1.ChangeLanguage(Form1.USER_LANGUAGE);
            InitializeComponent();
        }

        private List<CheckBox> _allCheckBoxes = new List<CheckBox>();
        private Label? _lblResult;
        private bool Changed;

        private void Permission_Load(object sender, EventArgs e)
        {
            ulong n = 0;
            if (Form1.NowSlash != null && Form1.NowSlash.default_member_permissions != null) n = (ulong)Form1.NowSlash.default_member_permissions;
            GeneratePermissionsUI(n);
            Changed = false;
        }

        private void GeneratePermissionsUI(ulong initialValue)
        {
            groupBox1.Controls.Clear();
            _allCheckBoxes.Clear();

            TableLayoutPanel table = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                AutoScroll = true
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));

            var flowGeneral = CreateSubPanel("一般權限 (General)");
            var flowText = CreateSubPanel("文字權限 (Text)");
            var flowVoice = CreateSubPanel("語音權限 (Voice)");

            table.Controls.Add(flowGeneral, 0, 0);
            table.Controls.Add(flowText, 1, 0);
            table.Controls.Add(flowVoice, 2, 0);
            groupBox1.Controls.Add(table);
            
            foreach (var perm in DiscordPermissionData.AllPermissions) {
                ulong bitmask = 1UL << perm.BitOffset;
                string name = perm.Name;
                if (Form1.USER_LANGUAGE == "zh-Hant" || Form1.USER_LANGUAGE == "zh-Hans") name = perm.LocalizedName;
                CheckBox cb = new CheckBox {
                    Text = name,
                    Tag = perm.BitOffset,
                    AutoSize = true,
                    Margin = new Padding(3, 5, 3, 5),
                    Checked = (initialValue & bitmask) != 0
                };

                ToolTip tt = new ToolTip();
                tt.SetToolTip(cb, perm.Name);

                cb.CheckedChanged += (s, e) => UpdateTotal();
                _allCheckBoxes.Add(cb);

                switch (perm.Category) {
                    case "General": flowGeneral.Controls.Add(cb); break;
                    case "Text": flowText.Controls.Add(cb); break;
                    case "Voice": flowVoice.Controls.Add(cb); break;
                }
            }

            _lblResult = new Label {
                Text = "權限值 (Permissions): 0",
                Dock = DockStyle.Bottom,
                Font = new Font("Consolas", 11, FontStyle.Bold),
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };
            this.Controls.Add(_lblResult);

            UpdateTotal();
        }

        private FlowLayoutPanel CreateSubPanel(string title)
        {
            FlowLayoutPanel p = new FlowLayoutPanel {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            p.Controls.Add(new Label { Text = title, Font = new Font(this.Font, FontStyle.Bold), AutoSize = true, Padding = new Padding(0, 5, 0, 5) });
            return p;
        }

        private void UpdateTotal()
        {
            ulong total = 0;
            foreach (var cb in _allCheckBoxes) {
                if (cb.Checked && cb.Tag is int offset) {
                    total |= (1UL << offset);
                }
            }
            _lblResult.Text = $"權限數值 (Permissions): {total}";
            Form1.NowSlash.default_member_permissions = total;
            Changed = true;
        }

        private void PermissionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Changed) this.DialogResult = DialogResult.OK;
            else this.DialogResult = DialogResult.Cancel;
        }
    }
}