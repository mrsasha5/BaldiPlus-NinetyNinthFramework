namespace NineNineFrameworkTool.Forms
{
    partial class FirstStartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FirstStartForm));
            _mainLabel = new Label();
            _readLicenseButton = new Button();
            SuspendLayout();
            // 
            // _mainLabel
            // 
            _mainLabel.Font = new Font("Segoe UI", 14F);
            _mainLabel.Location = new Point(12, 9);
            _mainLabel.Name = "_mainLabel";
            _mainLabel.Size = new Size(776, 397);
            _mainLabel.TabIndex = 0;
            _mainLabel.Text = resources.GetString("_mainLabel.Text");
            _mainLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _readLicenseButton
            // 
            _readLicenseButton.Location = new Point(635, 409);
            _readLicenseButton.Name = "_readLicenseButton";
            _readLicenseButton.Size = new Size(153, 29);
            _readLicenseButton.TabIndex = 1;
            _readLicenseButton.Text = "Read the License";
            _readLicenseButton.UseVisualStyleBackColor = true;
            _readLicenseButton.Click += ReadLicenseButton_Click;
            // 
            // FirstStartForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_readLicenseButton);
            Controls.Add(_mainLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "FirstStartForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome!";
            ResumeLayout(false);
        }

        #endregion

        private Label _mainLabel;
        private Button _readLicenseButton;
    }
}