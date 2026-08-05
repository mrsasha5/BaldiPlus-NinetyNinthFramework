namespace NineNineFrameworkTool.Forms
{
    partial class LicenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm));
            _continueButton = new Button();
            _licenseCheckBox = new CheckBox();
            _richTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // _continueButton
            // 
            _continueButton.Enabled = false;
            _continueButton.Location = new Point(708, 445);
            _continueButton.Name = "_continueButton";
            _continueButton.Size = new Size(123, 29);
            _continueButton.TabIndex = 1;
            _continueButton.Text = "Continue";
            _continueButton.UseVisualStyleBackColor = true;
            _continueButton.Click += ContinueButton_Click;
            // 
            // _licenseCheckBox
            // 
            _licenseCheckBox.Font = new Font("Segoe UI", 9F);
            _licenseCheckBox.Location = new Point(611, 445);
            _licenseCheckBox.Name = "_licenseCheckBox";
            _licenseCheckBox.Size = new Size(91, 29);
            _licenseCheckBox.TabIndex = 2;
            _licenseCheckBox.Text = "I Accept";
            _licenseCheckBox.UseVisualStyleBackColor = true;
            _licenseCheckBox.CheckedChanged += LicenseCheckBox_CheckedChanged;
            // 
            // _richTextBox
            // 
            _richTextBox.BorderStyle = BorderStyle.FixedSingle;
            _richTextBox.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
            _richTextBox.Location = new Point(12, 12);
            _richTextBox.Name = "_richTextBox";
            _richTextBox.ReadOnly = true;
            _richTextBox.RightToLeft = RightToLeft.No;
            _richTextBox.Size = new Size(820, 427);
            _richTextBox.TabIndex = 3;
            _richTextBox.Text = resources.GetString("_richTextBox.Text");
            // 
            // LicenseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 481);
            Controls.Add(_richTextBox);
            Controls.Add(_licenseCheckBox);
            Controls.Add(_continueButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LicenseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "License";
            ResumeLayout(false);
        }

        #endregion
        private Button _continueButton;
        private CheckBox _licenseCheckBox;
        private RichTextBox _richTextBox;
    }
}