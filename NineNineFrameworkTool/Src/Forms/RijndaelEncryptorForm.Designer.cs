namespace NineNineFrameworkTool.Forms
{
    partial class RijndaelEncryptorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RijndaelEncryptorForm));
            _inputLabel = new Label();
            _inputRichTextBox = new RichTextBox();
            _outputLabel = new Label();
            _outputRichTextBox = new RichTextBox();
            _modeComboBox = new ComboBox();
            _handleButton = new Button();
            _copyToBufferButton = new Button();
            SuspendLayout();
            // 
            // _inputLabel
            // 
            _inputLabel.Location = new Point(12, 9);
            _inputLabel.Name = "_inputLabel";
            _inputLabel.Size = new Size(156, 25);
            _inputLabel.TabIndex = 1;
            _inputLabel.Text = "Input:";
            // 
            // _inputRichTextBox
            // 
            _inputRichTextBox.Location = new Point(12, 37);
            _inputRichTextBox.Name = "_inputRichTextBox";
            _inputRichTextBox.Size = new Size(726, 187);
            _inputRichTextBox.TabIndex = 2;
            _inputRichTextBox.Text = "";
            // 
            // _outputLabel
            // 
            _outputLabel.Location = new Point(12, 227);
            _outputLabel.Name = "_outputLabel";
            _outputLabel.Size = new Size(156, 25);
            _outputLabel.TabIndex = 3;
            _outputLabel.Text = "Output:";
            // 
            // _outputRichTextBox
            // 
            _outputRichTextBox.Location = new Point(12, 255);
            _outputRichTextBox.Name = "_outputRichTextBox";
            _outputRichTextBox.ReadOnly = true;
            _outputRichTextBox.Size = new Size(726, 163);
            _outputRichTextBox.TabIndex = 4;
            _outputRichTextBox.Text = "";
            // 
            // _modeComboBox
            // 
            _modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _modeComboBox.FormattingEnabled = true;
            _modeComboBox.Items.AddRange(new object[] { "Encrypt", "Decrypt" });
            _modeComboBox.Location = new Point(402, 6);
            _modeComboBox.Name = "_modeComboBox";
            _modeComboBox.Size = new Size(127, 28);
            _modeComboBox.TabIndex = 5;
            // 
            // _handleButton
            // 
            _handleButton.Location = new Point(535, 6);
            _handleButton.Name = "_handleButton";
            _handleButton.Size = new Size(203, 28);
            _handleButton.TabIndex = 6;
            _handleButton.Text = "Handle";
            _handleButton.UseVisualStyleBackColor = true;
            _handleButton.Click += HandleButton_Click;
            // 
            // _copyToBufferButton
            // 
            _copyToBufferButton.Location = new Point(548, 424);
            _copyToBufferButton.Name = "_copyToBufferButton";
            _copyToBufferButton.Size = new Size(190, 29);
            _copyToBufferButton.TabIndex = 7;
            _copyToBufferButton.Text = "Copy to Buffer";
            _copyToBufferButton.UseVisualStyleBackColor = true;
            _copyToBufferButton.Click += CopyToBufferButton_Click;
            // 
            // RijndaelEncryptorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(750, 465);
            Controls.Add(_copyToBufferButton);
            Controls.Add(_handleButton);
            Controls.Add(_modeComboBox);
            Controls.Add(_outputRichTextBox);
            Controls.Add(_outputLabel);
            Controls.Add(_inputRichTextBox);
            Controls.Add(_inputLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "RijndaelEncryptorForm";
            Text = "Localization Encoder & Decoder";
            ResumeLayout(false);
        }

        #endregion

        private Label _inputLabel;
        private RichTextBox _inputRichTextBox;
        private Label _outputLabel;
        private RichTextBox _outputRichTextBox;
        private ComboBox _modeComboBox;
        private Button _handleButton;
        private Button _copyToBufferButton;
    }
}