namespace NineNineFrameworkTool.Forms
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            _textureSpriteButton = new Button();
            _notifyIconCheckBox = new CheckBox();
            _notifyIcon = new NotifyIcon(components);
            _contextMenuStrip = new ContextMenuStrip(components);
            _jsonAssetToolStripMenuItem = new ToolStripMenuItem();
            _toolStripSeparator2 = new ToolStripSeparator();
            _closeAppToolStripMenuItem = new ToolStripMenuItem();
            _achievementsButton = new Button();
            _jsonLabelTitleLabel = new Label();
            _unityLabel = new Label();
            _additionsLabel = new Label();
            _otherToolsTitleLabel = new Label();
            _textEncryptorButton = new Button();
            _contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _textureSpriteButton
            // 
            _textureSpriteButton.Location = new Point(12, 80);
            _textureSpriteButton.Name = "_textureSpriteButton";
            _textureSpriteButton.Size = new Size(261, 46);
            _textureSpriteButton.TabIndex = 4;
            _textureSpriteButton.Text = "Open Asset JSON Editor";
            _textureSpriteButton.UseVisualStyleBackColor = true;
            _textureSpriteButton.Click += TextureSpriteButton_Click;
            // 
            // _notifyIconCheckBox
            // 
            _notifyIconCheckBox.Checked = true;
            _notifyIconCheckBox.CheckState = CheckState.Checked;
            _notifyIconCheckBox.Location = new Point(774, 451);
            _notifyIconCheckBox.Name = "_notifyIconCheckBox";
            _notifyIconCheckBox.Size = new Size(156, 30);
            _notifyIconCheckBox.TabIndex = 10;
            _notifyIconCheckBox.Text = "Enable Notify Icon";
            _notifyIconCheckBox.UseVisualStyleBackColor = true;
            // 
            // _notifyIcon
            // 
            _notifyIcon.ContextMenuStrip = _contextMenuStrip;
            _notifyIcon.Icon = (Icon)resources.GetObject("_notifyIcon.Icon");
            _notifyIcon.Text = "Ninety-Ninth Framework GUI Tool";
            _notifyIcon.Visible = true;
            // 
            // _contextMenuStrip
            // 
            _contextMenuStrip.DropShadowEnabled = false;
            _contextMenuStrip.Font = new Font("Segoe UI", 9F);
            _contextMenuStrip.ImageScalingSize = new Size(20, 20);
            _contextMenuStrip.Items.AddRange(new ToolStripItem[] { _jsonAssetToolStripMenuItem, _toolStripSeparator2, _closeAppToolStripMenuItem });
            _contextMenuStrip.Name = "_contextMenuStrip";
            _contextMenuStrip.Size = new Size(197, 58);
            _contextMenuStrip.Text = "Ninety-Nine Framework Tools";
            // 
            // _jsonAssetToolStripMenuItem
            // 
            _jsonAssetToolStripMenuItem.Name = "_jsonAssetToolStripMenuItem";
            _jsonAssetToolStripMenuItem.Size = new Size(196, 24);
            _jsonAssetToolStripMenuItem.Text = "JSON Asset Editor";
            _jsonAssetToolStripMenuItem.Click += JsonAssetToolStripMenuItem_Click;
            // 
            // _toolStripSeparator2
            // 
            _toolStripSeparator2.Name = "_toolStripSeparator2";
            _toolStripSeparator2.Size = new Size(193, 6);
            // 
            // _closeAppToolStripMenuItem
            // 
            _closeAppToolStripMenuItem.Name = "_closeAppToolStripMenuItem";
            _closeAppToolStripMenuItem.Size = new Size(196, 24);
            _closeAppToolStripMenuItem.Text = "Close App";
            _closeAppToolStripMenuItem.Click += CloseAppToolStripMenuItem_Click;
            // 
            // _achievementsButton
            // 
            _achievementsButton.Location = new Point(279, 80);
            _achievementsButton.Name = "_achievementsButton";
            _achievementsButton.Size = new Size(261, 46);
            _achievementsButton.TabIndex = 11;
            _achievementsButton.Text = "Open Achievement Editor";
            _achievementsButton.UseVisualStyleBackColor = true;
            // 
            // _jsonLabelTitleLabel
            // 
            _jsonLabelTitleLabel.Font = new Font("Segoe UI", 12F);
            _jsonLabelTitleLabel.Location = new Point(12, 5);
            _jsonLabelTitleLabel.Name = "_jsonLabelTitleLabel";
            _jsonLabelTitleLabel.Size = new Size(261, 43);
            _jsonLabelTitleLabel.TabIndex = 12;
            _jsonLabelTitleLabel.Text = "JSON Management";
            _jsonLabelTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _unityLabel
            // 
            _unityLabel.Location = new Point(12, 52);
            _unityLabel.Name = "_unityLabel";
            _unityLabel.Size = new Size(261, 25);
            _unityLabel.TabIndex = 13;
            _unityLabel.Text = "Unity Assets";
            // 
            // _additionsLabel
            // 
            _additionsLabel.Location = new Point(279, 52);
            _additionsLabel.Name = "_additionsLabel";
            _additionsLabel.Size = new Size(261, 25);
            _additionsLabel.TabIndex = 14;
            _additionsLabel.Text = "Framework Additions";
            // 
            // _otherToolsTitleLabel
            // 
            _otherToolsTitleLabel.Font = new Font("Segoe UI", 12F);
            _otherToolsTitleLabel.Location = new Point(12, 129);
            _otherToolsTitleLabel.Name = "_otherToolsTitleLabel";
            _otherToolsTitleLabel.Size = new Size(261, 43);
            _otherToolsTitleLabel.TabIndex = 16;
            _otherToolsTitleLabel.Text = "Other Tools";
            _otherToolsTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _textEncryptorButton
            // 
            _textEncryptorButton.Location = new Point(12, 175);
            _textEncryptorButton.Name = "_textEncryptorButton";
            _textEncryptorButton.Size = new Size(261, 46);
            _textEncryptorButton.TabIndex = 17;
            _textEncryptorButton.Text = "Open Localization Encryptor";
            _textEncryptorButton.UseVisualStyleBackColor = true;
            _textEncryptorButton.Click += TextEncryptorButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 493);
            Controls.Add(_textEncryptorButton);
            Controls.Add(_otherToolsTitleLabel);
            Controls.Add(_additionsLabel);
            Controls.Add(_unityLabel);
            Controls.Add(_jsonLabelTitleLabel);
            Controls.Add(_achievementsButton);
            Controls.Add(_notifyIconCheckBox);
            Controls.Add(_textureSpriteButton);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ninety-Ninth Framework's Tool";
            _contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button _textureSpriteButton;
        private CheckBox _notifyIconCheckBox;
        private NotifyIcon _notifyIcon;
        private ContextMenuStrip _contextMenuStrip;
        private ToolStripMenuItem _jsonAssetToolStripMenuItem;
        private ToolStripSeparator _toolStripSeparator2;
        private ToolStripMenuItem _closeAppToolStripMenuItem;
        private Button _achievementsButton;
        private Label _jsonLabelTitleLabel;
        private Label _unityLabel;
        private Label _additionsLabel;
        private Label _otherToolsTitleLabel;
        private Button _textEncryptorButton;
    }
}