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
            resources.ApplyResources(button1, "button1");
            button1.Image = Images.plus;
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Add_Click;
            // 
            // comboBox1
            // 
            resources.ApplyResources(comboBox1, "comboBox1");
            comboBox1.FormattingEnabled = true;
            comboBox1.Name = "comboBox1";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // LocalizationForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LocalizationForm";
            FormClosing += LocalizationForm_FormClosing;
            Load += LocalizationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
    }
}