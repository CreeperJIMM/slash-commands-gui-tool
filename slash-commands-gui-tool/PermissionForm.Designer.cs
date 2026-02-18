namespace slash_commands_gui_tool
{
    partial class PermissionForm
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
            groupBox1 = new GroupBox();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(608, 539);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            // 
            // PermissionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 577);
            Controls.Add(groupBox1);
            Name = "PermissionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bot Permission";
            FormClosing += this.PermissionForm_FormClosing;
            Load += Permission_Load;
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
    }
}