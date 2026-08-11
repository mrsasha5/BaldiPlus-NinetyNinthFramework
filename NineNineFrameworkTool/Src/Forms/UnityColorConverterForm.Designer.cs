namespace NineNineFrameworkTool.Forms
{
    partial class UnityColorConverterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnityColorConverterForm));
            _colorDialog = new ColorDialog();
            _inputTextBox = new TextBox();
            _colorLabel = new Label();
            _paletteButton = new Button();
            _outputLabel = new Label();
            _outputTextBox = new TextBox();
            _startButton = new Button();
            SuspendLayout();
            // 
            // _inputTextBox
            // 
            _inputTextBox.Font = new Font("Segoe UI", 12F);
            _inputTextBox.Location = new Point(12, 47);
            _inputTextBox.Name = "_inputTextBox";
            _inputTextBox.Size = new Size(189, 34);
            _inputTextBox.TabIndex = 1;
            // 
            // _colorLabel
            // 
            _colorLabel.Font = new Font("Segoe UI", 12F);
            _colorLabel.Location = new Point(12, 9);
            _colorLabel.Name = "_colorLabel";
            _colorLabel.Size = new Size(189, 35);
            _colorLabel.TabIndex = 2;
            _colorLabel.Text = "The color:";
            // 
            // _paletteButton
            // 
            _paletteButton.Location = new Point(207, 47);
            _paletteButton.Name = "_paletteButton";
            _paletteButton.Size = new Size(94, 34);
            _paletteButton.TabIndex = 3;
            _paletteButton.Text = "Palette";
            _paletteButton.UseVisualStyleBackColor = true;
            _paletteButton.Click += PaletteButton_Click;
            // 
            // _outputLabel
            // 
            _outputLabel.Font = new Font("Segoe UI", 12F);
            _outputLabel.Location = new Point(12, 89);
            _outputLabel.Name = "_outputLabel";
            _outputLabel.Size = new Size(189, 35);
            _outputLabel.TabIndex = 5;
            _outputLabel.Text = "Output:";
            // 
            // _outputTextBox
            // 
            _outputTextBox.Font = new Font("Segoe UI", 12F);
            _outputTextBox.Location = new Point(12, 127);
            _outputTextBox.Name = "_outputTextBox";
            _outputTextBox.ReadOnly = true;
            _outputTextBox.Size = new Size(530, 34);
            _outputTextBox.TabIndex = 4;
            // 
            // _startButton
            // 
            _startButton.Location = new Point(307, 47);
            _startButton.Name = "_startButton";
            _startButton.Size = new Size(235, 34);
            _startButton.TabIndex = 6;
            _startButton.Text = "Normalize";
            _startButton.UseVisualStyleBackColor = true;
            _startButton.Click += StartButton_Click;
            // 
            // UnityColorConverterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 178);
            Controls.Add(_startButton);
            Controls.Add(_outputLabel);
            Controls.Add(_outputTextBox);
            Controls.Add(_paletteButton);
            Controls.Add(_colorLabel);
            Controls.Add(_inputTextBox);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "UnityColorConverterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Unity RGBA Color Normalizer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ColorDialog _colorDialog;
        private TextBox _inputTextBox;
        private Label _colorLabel;
        private Button _paletteButton;
        private Label _outputLabel;
        private TextBox _outputTextBox;
        private Button _startButton;
    }
}