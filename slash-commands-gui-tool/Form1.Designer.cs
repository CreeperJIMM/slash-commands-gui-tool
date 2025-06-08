namespace slash_commands_gui_tool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            toolStripComboBox1 = new ToolStripComboBox();
            AutoSyncStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            disableDToolStripMenuItem = new ToolStripMenuItem();
            newClientToolStripMenuItem = new ToolStripMenuItem();
            oToolStripMenuItem = new ToolStripMenuItem();
            backupBotToLocalToolStripMenuItem = new ToolStripMenuItem();
            copyAllLocalToBotToolStripMenuItem = new ToolStripMenuItem();
            localCommandSaveFolderToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            languagesToolStripMenuItem = new ToolStripMenuItem();
            englishToolStripMenuItem = new ToolStripMenuItem();
            chinese繁體中文ToolStripMenuItem = new ToolStripMenuItem();
            chinese簡體中文SToolStripMenuItem = new ToolStripMenuItem();
            uIThemeToolStripMenuItem = new ToolStripMenuItem();
            systemSToolStripMenuItem = new ToolStripMenuItem();
            whiteWToolStripMenuItem = new ToolStripMenuItem();
            darkDToolStripMenuItem = new ToolStripMenuItem();
            othersToolStripMenuItem = new ToolStripMenuItem();
            resetAppliToolStripMenuItem = new ToolStripMenuItem();
            clearApplicationsAndCloseToolStripMenuItem = new ToolStripMenuItem();
            aboutIToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            listBox1 = new ListBox();
            plusbutton2 = new Button();
            minusbutton2 = new Button();
            groupBox2 = new GroupBox();
            savebutton = new Button();
            listBox2 = new ListBox();
            textBox2 = new TextBox();
            loadbutton = new Button();
            backbutton = new Button();
            groupBox1 = new GroupBox();
            label2 = new Label();
            textBox1 = new TextBox();
            minusbutton1 = new Button();
            plusbutton1 = new Button();
            groupBox3 = new GroupBox();
            updateButton = new Button();
            addoptionButton = new Button();
            cmdtypeLabel = new Label();
            comboBox1 = new ComboBox();
            groupBox6 = new GroupBox();
            checkBox3 = new CheckBox();
            editchoiceButton = new Button();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button4 = new Button();
            groupBox5 = new GroupBox();
            linkLabel1 = new LinkLabel();
            localizationButton2 = new Button();
            localizationButton1 = new Button();
            cmdoptionLabel = new Label();
            cmddescTextbox = new TextBox();
            cmddescLabel = new Label();
            cmdnameTextbox = new TextBox();
            cmdnameLabel = new Label();
            listBox3 = new ListBox();
            groupBox4 = new GroupBox();
            label1 = new Label();
            button2 = new Button();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            reloadButton = new Button();
            menuStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(28, 28);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripComboBox1, AutoSyncStripMenuItem, newClientToolStripMenuItem, oToolStripMenuItem, settingsToolStripMenuItem, aboutIToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // toolStripComboBox1
            // 
            toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            toolStripComboBox1.DropDownWidth = 180;
            toolStripComboBox1.Name = "toolStripComboBox1";
            resources.ApplyResources(toolStripComboBox1, "toolStripComboBox1");
            toolStripComboBox1.SelectedIndexChanged += ClientSelected;
            // 
            // AutoSyncStripMenuItem
            // 
            AutoSyncStripMenuItem.BackColor = SystemColors.Control;
            AutoSyncStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, disableDToolStripMenuItem });
            AutoSyncStripMenuItem.Name = "AutoSyncStripMenuItem";
            resources.ApplyResources(AutoSyncStripMenuItem, "AutoSyncStripMenuItem");
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            toolStripMenuItem1.Tag = "enable";
            toolStripMenuItem1.Click += autoSync_Click;
            // 
            // disableDToolStripMenuItem
            // 
            disableDToolStripMenuItem.Name = "disableDToolStripMenuItem";
            resources.ApplyResources(disableDToolStripMenuItem, "disableDToolStripMenuItem");
            disableDToolStripMenuItem.Tag = "disable";
            disableDToolStripMenuItem.Click += autoSync_Click;
            // 
            // newClientToolStripMenuItem
            // 
            newClientToolStripMenuItem.Name = "newClientToolStripMenuItem";
            resources.ApplyResources(newClientToolStripMenuItem, "newClientToolStripMenuItem");
            newClientToolStripMenuItem.Click += CreateClient_Click;
            // 
            // oToolStripMenuItem
            // 
            oToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backupBotToLocalToolStripMenuItem, copyAllLocalToBotToolStripMenuItem, localCommandSaveFolderToolStripMenuItem });
            oToolStripMenuItem.Name = "oToolStripMenuItem";
            resources.ApplyResources(oToolStripMenuItem, "oToolStripMenuItem");
            // 
            // backupBotToLocalToolStripMenuItem
            // 
            backupBotToLocalToolStripMenuItem.Name = "backupBotToLocalToolStripMenuItem";
            resources.ApplyResources(backupBotToLocalToolStripMenuItem, "backupBotToLocalToolStripMenuItem");
            backupBotToLocalToolStripMenuItem.Click += backupToLocal_Click;
            // 
            // copyAllLocalToBotToolStripMenuItem
            // 
            copyAllLocalToBotToolStripMenuItem.Name = "copyAllLocalToBotToolStripMenuItem";
            resources.ApplyResources(copyAllLocalToBotToolStripMenuItem, "copyAllLocalToBotToolStripMenuItem");
            copyAllLocalToBotToolStripMenuItem.Click += PushToBot_Click;
            // 
            // localCommandSaveFolderToolStripMenuItem
            // 
            localCommandSaveFolderToolStripMenuItem.Name = "localCommandSaveFolderToolStripMenuItem";
            resources.ApplyResources(localCommandSaveFolderToolStripMenuItem, "localCommandSaveFolderToolStripMenuItem");
            localCommandSaveFolderToolStripMenuItem.Click += OpenFolder_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { languagesToolStripMenuItem, uIThemeToolStripMenuItem, othersToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // languagesToolStripMenuItem
            // 
            languagesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { englishToolStripMenuItem, chinese繁體中文ToolStripMenuItem, chinese簡體中文SToolStripMenuItem });
            languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            resources.ApplyResources(languagesToolStripMenuItem, "languagesToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(englishToolStripMenuItem, "englishToolStripMenuItem");
            englishToolStripMenuItem.Tag = "en-US";
            englishToolStripMenuItem.Click += changeLanguage_Click;
            // 
            // chinese繁體中文ToolStripMenuItem
            // 
            chinese繁體中文ToolStripMenuItem.Name = "chinese繁體中文ToolStripMenuItem";
            resources.ApplyResources(chinese繁體中文ToolStripMenuItem, "chinese繁體中文ToolStripMenuItem");
            chinese繁體中文ToolStripMenuItem.Tag = "zh-TW";
            chinese繁體中文ToolStripMenuItem.Click += changeLanguage_Click;
            // 
            // chinese簡體中文SToolStripMenuItem
            // 
            chinese簡體中文SToolStripMenuItem.Name = "chinese簡體中文SToolStripMenuItem";
            resources.ApplyResources(chinese簡體中文SToolStripMenuItem, "chinese簡體中文SToolStripMenuItem");
            chinese簡體中文SToolStripMenuItem.Tag = "zh-CN";
            chinese簡體中文SToolStripMenuItem.Click += changeLanguage_Click;
            // 
            // uIThemeToolStripMenuItem
            // 
            uIThemeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { systemSToolStripMenuItem, whiteWToolStripMenuItem, darkDToolStripMenuItem });
            uIThemeToolStripMenuItem.Name = "uIThemeToolStripMenuItem";
            resources.ApplyResources(uIThemeToolStripMenuItem, "uIThemeToolStripMenuItem");
            // 
            // systemSToolStripMenuItem
            // 
            systemSToolStripMenuItem.Name = "systemSToolStripMenuItem";
            resources.ApplyResources(systemSToolStripMenuItem, "systemSToolStripMenuItem");
            systemSToolStripMenuItem.Tag = "System";
            systemSToolStripMenuItem.Click += ThemeMode_click;
            // 
            // whiteWToolStripMenuItem
            // 
            whiteWToolStripMenuItem.Name = "whiteWToolStripMenuItem";
            resources.ApplyResources(whiteWToolStripMenuItem, "whiteWToolStripMenuItem");
            whiteWToolStripMenuItem.Tag = "White";
            whiteWToolStripMenuItem.Click += ThemeMode_click;
            // 
            // darkDToolStripMenuItem
            // 
            darkDToolStripMenuItem.Name = "darkDToolStripMenuItem";
            resources.ApplyResources(darkDToolStripMenuItem, "darkDToolStripMenuItem");
            darkDToolStripMenuItem.Tag = "Dark";
            darkDToolStripMenuItem.Click += ThemeMode_click;
            // 
            // othersToolStripMenuItem
            // 
            othersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { resetAppliToolStripMenuItem, clearApplicationsAndCloseToolStripMenuItem });
            othersToolStripMenuItem.ForeColor = Color.Red;
            othersToolStripMenuItem.Name = "othersToolStripMenuItem";
            resources.ApplyResources(othersToolStripMenuItem, "othersToolStripMenuItem");
            // 
            // resetAppliToolStripMenuItem
            // 
            resetAppliToolStripMenuItem.ForeColor = Color.Red;
            resetAppliToolStripMenuItem.Name = "resetAppliToolStripMenuItem";
            resources.ApplyResources(resetAppliToolStripMenuItem, "resetAppliToolStripMenuItem");
            resetAppliToolStripMenuItem.Click += ResetDatabase;
            // 
            // clearApplicationsAndCloseToolStripMenuItem
            // 
            clearApplicationsAndCloseToolStripMenuItem.ForeColor = Color.Red;
            clearApplicationsAndCloseToolStripMenuItem.Name = "clearApplicationsAndCloseToolStripMenuItem";
            resources.ApplyResources(clearApplicationsAndCloseToolStripMenuItem, "clearApplicationsAndCloseToolStripMenuItem");
            clearApplicationsAndCloseToolStripMenuItem.Click += ClearLocalAndExit;
            // 
            // aboutIToolStripMenuItem
            // 
            aboutIToolStripMenuItem.Name = "aboutIToolStripMenuItem";
            resources.ApplyResources(aboutIToolStripMenuItem, "aboutIToolStripMenuItem");
            aboutIToolStripMenuItem.Click += about_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 192, 192);
            resources.ApplyResources(button1, "button1");
            button1.FlatAppearance.BorderSize = 0;
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            resources.ApplyResources(listBox1, "listBox1");
            listBox1.Name = "listBox1";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
            listBox1.Tag = "Bot";
            listBox1.SelectedIndexChanged += listboxSelect;
            // 
            // plusbutton2
            // 
            plusbutton2.BackColor = Color.FromArgb(128, 255, 128);
            plusbutton2.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(plusbutton2, "plusbutton2");
            plusbutton2.Name = "plusbutton2";
            plusbutton2.Tag = "Local";
            plusbutton2.UseVisualStyleBackColor = false;
            plusbutton2.Click += plusbutton_Click;
            // 
            // minusbutton2
            // 
            minusbutton2.BackColor = Color.FromArgb(255, 128, 128);
            minusbutton2.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(minusbutton2, "minusbutton2");
            minusbutton2.Name = "minusbutton2";
            minusbutton2.Tag = "Local";
            minusbutton2.UseVisualStyleBackColor = false;
            minusbutton2.Click += minusbutton_ClickAsync;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(savebutton);
            groupBox2.Controls.Add(listBox2);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(loadbutton);
            groupBox2.Controls.Add(minusbutton2);
            groupBox2.Controls.Add(plusbutton2);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // savebutton
            // 
            resources.ApplyResources(savebutton, "savebutton");
            savebutton.Name = "savebutton";
            savebutton.UseVisualStyleBackColor = true;
            savebutton.Click += SaveFile_Click;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            resources.ApplyResources(listBox2, "listBox2");
            listBox2.Name = "listBox2";
            listBox2.SelectionMode = SelectionMode.MultiExtended;
            listBox2.Tag = "Local";
            listBox2.SelectedIndexChanged += listboxSelect;
            // 
            // textBox2
            // 
            resources.ApplyResources(textBox2, "textBox2");
            textBox2.Name = "textBox2";
            // 
            // loadbutton
            // 
            resources.ApplyResources(loadbutton, "loadbutton");
            loadbutton.Name = "loadbutton";
            loadbutton.UseVisualStyleBackColor = true;
            loadbutton.Click += LoadFile_Click;
            // 
            // backbutton
            // 
            resources.ApplyResources(backbutton, "backbutton");
            backbutton.Image = Images.back;
            backbutton.Name = "backbutton";
            backbutton.UseVisualStyleBackColor = true;
            backbutton.Click += BackOption_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(minusbutton1);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(plusbutton1);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // minusbutton1
            // 
            minusbutton1.BackColor = Color.FromArgb(255, 128, 128);
            minusbutton1.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(minusbutton1, "minusbutton1");
            minusbutton1.Name = "minusbutton1";
            minusbutton1.Tag = "Bot";
            minusbutton1.UseVisualStyleBackColor = false;
            minusbutton1.Click += minusbutton_ClickAsync;
            // 
            // plusbutton1
            // 
            plusbutton1.BackColor = Color.FromArgb(128, 255, 128);
            plusbutton1.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(plusbutton1, "plusbutton1");
            plusbutton1.Name = "plusbutton1";
            plusbutton1.Tag = "Bot";
            plusbutton1.UseVisualStyleBackColor = false;
            plusbutton1.Click += plusbutton_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(updateButton);
            groupBox3.Controls.Add(addoptionButton);
            groupBox3.Controls.Add(cmdtypeLabel);
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(groupBox6);
            groupBox3.Controls.Add(button4);
            groupBox3.Controls.Add(backbutton);
            groupBox3.Controls.Add(groupBox5);
            groupBox3.Controls.Add(localizationButton2);
            groupBox3.Controls.Add(localizationButton1);
            groupBox3.Controls.Add(cmdoptionLabel);
            groupBox3.Controls.Add(cmddescTextbox);
            groupBox3.Controls.Add(cmddescLabel);
            groupBox3.Controls.Add(cmdnameTextbox);
            groupBox3.Controls.Add(cmdnameLabel);
            groupBox3.Controls.Add(listBox3);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // updateButton
            // 
            resources.ApplyResources(updateButton, "updateButton");
            updateButton.Image = Images.saved;
            updateButton.Name = "updateButton";
            updateButton.UseVisualStyleBackColor = true;
            updateButton.Click += updateButton_Click;
            // 
            // addoptionButton
            // 
            resources.ApplyResources(addoptionButton, "addoptionButton");
            addoptionButton.Image = Images.edit;
            addoptionButton.Name = "addoptionButton";
            addoptionButton.UseVisualStyleBackColor = true;
            addoptionButton.Click += editOptionButton;
            // 
            // cmdtypeLabel
            // 
            resources.ApplyResources(cmdtypeLabel, "cmdtypeLabel");
            cmdtypeLabel.Name = "cmdtypeLabel";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBox1, "comboBox1");
            comboBox1.FormattingEnabled = true;
            comboBox1.Name = "comboBox1";
            comboBox1.SelectedIndexChanged += TypeChangedEvent;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(checkBox3);
            groupBox6.Controls.Add(editchoiceButton);
            groupBox6.Controls.Add(checkBox2);
            groupBox6.Controls.Add(checkBox1);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // checkBox3
            // 
            resources.ApplyResources(checkBox3, "checkBox3");
            checkBox3.Name = "checkBox3";
            checkBox3.Tag = "nsfw";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.Click += checkBox_Click;
            // 
            // editchoiceButton
            // 
            resources.ApplyResources(editchoiceButton, "editchoiceButton");
            editchoiceButton.Image = Images.choice2x;
            editchoiceButton.Name = "editchoiceButton";
            editchoiceButton.UseVisualStyleBackColor = true;
            editchoiceButton.Click += editchoiceButton_Click;
            // 
            // checkBox2
            // 
            resources.ApplyResources(checkBox2, "checkBox2");
            checkBox2.Name = "checkBox2";
            checkBox2.Tag = "autocomplete";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.Click += checkBox_Click;
            // 
            // checkBox1
            // 
            resources.ApplyResources(checkBox1, "checkBox1");
            checkBox1.Name = "checkBox1";
            checkBox1.Tag = "required";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.Click += checkBox_Click;
            // 
            // button4
            // 
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(linkLabel1);
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            // 
            // linkLabel1
            // 
            resources.ApplyResources(linkLabel1, "linkLabel1");
            linkLabel1.Name = "linkLabel1";
            linkLabel1.TabStop = true;
            linkLabel1.VisitedLinkColor = Color.FromArgb(128, 128, 255);
            // 
            // localizationButton2
            // 
            localizationButton2.Image = Images.localizations;
            resources.ApplyResources(localizationButton2, "localizationButton2");
            localizationButton2.Name = "localizationButton2";
            localizationButton2.Tag = "description";
            localizationButton2.UseVisualStyleBackColor = true;
            localizationButton2.Click += localization_Click;
            // 
            // localizationButton1
            // 
            localizationButton1.Image = Images.localizations;
            resources.ApplyResources(localizationButton1, "localizationButton1");
            localizationButton1.Name = "localizationButton1";
            localizationButton1.Tag = "name";
            localizationButton1.UseVisualStyleBackColor = true;
            localizationButton1.Click += localization_Click;
            // 
            // cmdoptionLabel
            // 
            resources.ApplyResources(cmdoptionLabel, "cmdoptionLabel");
            cmdoptionLabel.Name = "cmdoptionLabel";
            // 
            // cmddescTextbox
            // 
            resources.ApplyResources(cmddescTextbox, "cmddescTextbox");
            cmddescTextbox.Name = "cmddescTextbox";
            cmddescTextbox.Tag = "description";
            cmddescTextbox.TextChanged += TextChangedEvent;
            // 
            // cmddescLabel
            // 
            resources.ApplyResources(cmddescLabel, "cmddescLabel");
            cmddescLabel.Name = "cmddescLabel";
            // 
            // cmdnameTextbox
            // 
            resources.ApplyResources(cmdnameTextbox, "cmdnameTextbox");
            cmdnameTextbox.Name = "cmdnameTextbox";
            cmdnameTextbox.ReadOnly = true;
            cmdnameTextbox.Tag = "name";
            cmdnameTextbox.TextChanged += TextChangedEvent;
            // 
            // cmdnameLabel
            // 
            resources.ApplyResources(cmdnameLabel, "cmdnameLabel");
            cmdnameLabel.Name = "cmdnameLabel";
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            resources.ApplyResources(listBox3, "listBox3");
            listBox3.Name = "listBox3";
            listBox3.MouseDoubleClick += listBox3_DoubleClick;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(button2);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(28, 28);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripProgressBar1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            statusStrip1.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(toolStripStatusLabel2, "toolStripStatusLabel2");
            toolStripStatusLabel2.Spring = true;
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // reloadButton
            // 
            reloadButton.BackColor = SystemColors.Control;
            reloadButton.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(reloadButton, "reloadButton");
            reloadButton.Name = "reloadButton";
            reloadButton.UseVisualStyleBackColor = false;
            reloadButton.Click += reloadButton_ClickAsync;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(reloadButton);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(button1);
            Controls.Add(menuStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            WindowState = FormWindowState.Maximized;
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += KeyEvent;
            Resize += Form1_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripMenuItem newClientToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem chinese繁體中文ToolStripMenuItem;
        private Button button1;
        private ListBox listBox1;
        private Button plusbutton2;
        private Button minusbutton2;
        private GroupBox groupBox2;
        private TextBox textBox2;
        private Button loadbutton;
        private GroupBox groupBox1;
        private TextBox textBox1;
        private ListBox listBox2;
        private Button minusbutton1;
        private Button plusbutton1;
        private Button savebutton;
        private ToolStripMenuItem oToolStripMenuItem;
        private ToolStripMenuItem copyAllLocalToBotToolStripMenuItem;
        private ToolStripMenuItem backupBotToLocalToolStripMenuItem;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Button button2;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripMenuItem AutoSyncStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem disableDToolStripMenuItem;
        private Label label1;
        private ToolStripMenuItem chinese簡體中文SToolStripMenuItem;
        private ToolStripMenuItem uIThemeToolStripMenuItem;
        private ToolStripMenuItem whiteWToolStripMenuItem;
        private ToolStripMenuItem darkDToolStripMenuItem;
        private Label label2;
        private ToolStripMenuItem systemSToolStripMenuItem;
        private LinkLabel linkLabel1;
        private ListBox listBox3;
        private Button backbutton;
        private TextBox cmddescTextbox;
        private Label cmddescLabel;
        private TextBox cmdnameTextbox;
        private Label cmdnameLabel;
        private Label cmdoptionLabel;
        private Button localizationButton1;
        private Button localizationButton2;
        private ToolStripMenuItem othersToolStripMenuItem;
        private ToolStripMenuItem resetAppliToolStripMenuItem;
        private ToolStripMenuItem clearApplicationsAndCloseToolStripMenuItem;
        private GroupBox groupBox5;
        private Button button4;
        private Label cmdtypeLabel;
        private ComboBox comboBox1;
        private GroupBox groupBox6;
        private Button addoptionButton;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button updateButton;
        private Button reloadButton;
        private Button editchoiceButton;
        private ToolStripMenuItem localCommandSaveFolderToolStripMenuItem;
        private ToolStripMenuItem aboutIToolStripMenuItem;
        private CheckBox checkBox3;
    }
}
