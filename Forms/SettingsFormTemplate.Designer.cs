namespace SirenSettingEdtor.Forms
{
    partial class SettingsFormTemplate
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
            if (disposing && (components != null))
            {
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
            this.cCam = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cDrag = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bCopy = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cCam
            // 
            this.cCam.Appearance = System.Windows.Forms.Appearance.Button;
            this.cCam.Location = new System.Drawing.Point(194, 6);
            this.cCam.Name = "cCam";
            this.cCam.Size = new System.Drawing.Size(26, 26);
            this.cCam.TabIndex = 38;
            this.cCam.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label7.Location = new System.Drawing.Point(12, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(176, 17);
            this.label7.TabIndex = 37;
            this.label7.Text = "Allow Camera Movement";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cDrag
            // 
            this.cDrag.Appearance = System.Windows.Forms.Appearance.Button;
            this.cDrag.Checked = true;
            this.cDrag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cDrag.Location = new System.Drawing.Point(194, 38);
            this.cDrag.Name = "cDrag";
            this.cDrag.Size = new System.Drawing.Size(26, 26);
            this.cDrag.TabIndex = 40;
            this.cDrag.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.label1.Location = new System.Drawing.Point(92, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 39;
            this.label1.Text = "Drag Camera";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bCopy
            // 
            this.bCopy.Font = new System.Drawing.Font("Courier New", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.bCopy.Location = new System.Drawing.Point(15, 83);
            this.bCopy.Name = "bCopy";
            this.bCopy.Size = new System.Drawing.Size(205, 42);
            this.bCopy.TabIndex = 106;
            this.bCopy.Text = "COPY";
            this.bCopy.UseVisualStyleBackColor = true;
            // 
            // bSave
            // 
            this.bSave.Font = new System.Drawing.Font("Courier New", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.bSave.Location = new System.Drawing.Point(15, 131);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(205, 42);
            this.bSave.TabIndex = 107;
            this.bSave.Text = "SAVE";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // SettingsFormTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 197);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bCopy);
            this.Controls.Add(this.cDrag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cCam);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingsFormTemplate";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cCam;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cDrag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bCopy;
        private System.Windows.Forms.Button bSave;
    }
}