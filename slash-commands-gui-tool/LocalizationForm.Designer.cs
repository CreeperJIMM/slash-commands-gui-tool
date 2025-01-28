namespace slash_commands_gui_tool
{
    partial class LocalizationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalizationForm));
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top;
            button1.Font = new Font("Microsoft JhengHei UI", 18F);
            button1.Image = Images.plus;
            button1.Location = new Point(285, 12);
            button1.Name = "button1";
            button1.Size = new Size(70, 60);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = true;
            button1.Click += this.Add_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Microsoft JhengHei UI", 12F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(30, 30);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(230, 28);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 12F);
            label1.Location = new Point(275, 60);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 2;
            // 
            // LocalizationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(384, 81);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LocalizationForm";
            Text = "Localization Config";
            FormClosing += this.LocalizationForm_FormClosing;
            Load += this.LocalizationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
    }
}