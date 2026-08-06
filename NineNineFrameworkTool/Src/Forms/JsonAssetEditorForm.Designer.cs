namespace NineNineFrameworkTool.Forms
{
    partial class JsonAssetEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonAssetEditorForm));
            _toolTipStandard = new ToolTip(components);
            _centerLabel = new Label();
            _assetNameLabel = new Label();
            _formatLabel = new Label();
            _assetTypeLabel = new Label();
            _soundKeyLabel = new Label();
            _soundTypeLabel = new Label();
            _volumeMultiplierLabel = new Label();
            _subtitleDurationLabel = new Label();
            _subtitleColorLabel = new Label();
            _addToMemoryCheckBox = new CheckBox();
            _additionalSoundKeysButton = new Button();
            _encryptedCheckBox = new CheckBox();
            _progressBar = new ProgressBar();
            _startButton = new Button();
            _fileListView = new ListView();
            _fileHeader = new ColumnHeader();
            _jsonHeader = new ColumnHeader();
            _assetTypeHeader = new ColumnHeader();
            _removeSelectedButton = new Button();
            _selectAllButton = new Button();
            _addFilesButton = new Button();
            _addFilesFromBufferButton = new Button();
            _tipLabel = new Label();
            _applyChangesButton = new Button();
            _viewingFileLabel = new Label();
            _textureSpritePanel = new Panel();
            _sprTitleLabel = new Label();
            _centerTextBox = new TextBox();
            _pixelsPerUnitTextBox = new TextBox();
            _pixelsPerUnitLabel = new Label();
            _formatComboBox = new ComboBox();
            _texTitleLabel = new Label();
            _assetNameTextBox = new TextBox();
            _soundPanel = new Panel();
            _soundPaletteButton = new Button();
            _soundTypeComboBox = new ComboBox();
            _subtitleDurationTextBox = new TextBox();
            _subtitleColorTextBox = new TextBox();
            _volumeTextBox = new TextBox();
            _soundKeyTextBox = new TextBox();
            _soundObjectTitleLabel = new Label();
            _assetBasicInfoPanel = new Panel();
            _assetTypeComboBox = new ComboBox();
            _addFilesFromFolderButton = new Button();
            _noPropertiesPanel = new Panel();
            _colorDialog = new ColorDialog();
            _deleteSelectedJsonsButton = new Button();
            _textureSpritePanel.SuspendLayout();
            _soundPanel.SuspendLayout();
            _assetBasicInfoPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _toolTipStandard
            // 
            _toolTipStandard.ToolTipIcon = ToolTipIcon.Info;
            _toolTipStandard.ToolTipTitle = "About Property";
            // 
            // _centerLabel
            // 
            _centerLabel.Font = new Font("Segoe UI", 12F);
            _centerLabel.Location = new Point(14, 271);
            _centerLabel.Name = "_centerLabel";
            _centerLabel.Size = new Size(190, 34);
            _centerLabel.TabIndex = 22;
            _centerLabel.Text = "Center:";
            _centerLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_centerLabel, "A 2D vector that controls the sprite's center.\r\nEach value must be in the range [0.0, 1.0],\r\nrepresenting normalized coordinates.");
            // 
            // _assetNameLabel
            // 
            _assetNameLabel.Font = new Font("Segoe UI", 12F);
            _assetNameLabel.Location = new Point(14, 62);
            _assetNameLabel.Name = "_assetNameLabel";
            _assetNameLabel.Size = new Size(190, 34);
            _assetNameLabel.TabIndex = 19;
            _assetNameLabel.Text = "Name:";
            _assetNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_assetNameLabel, "Overrides the default asset name to prevent it from using the file name.");
            // 
            // _formatLabel
            // 
            _formatLabel.Font = new Font("Segoe UI", 12F);
            _formatLabel.Location = new Point(14, 130);
            _formatLabel.Name = "_formatLabel";
            _formatLabel.Size = new Size(190, 36);
            _formatLabel.TabIndex = 15;
            _formatLabel.Text = "Format:";
            _formatLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_formatLabel, "Defines compression and other rendering parameters for the Unity engine.");
            // 
            // _assetTypeLabel
            // 
            _assetTypeLabel.Font = new Font("Segoe UI", 12F);
            _assetTypeLabel.Location = new Point(14, 15);
            _assetTypeLabel.Name = "_assetTypeLabel";
            _assetTypeLabel.Size = new Size(190, 36);
            _assetTypeLabel.TabIndex = 16;
            _assetTypeLabel.Text = "Asset type:";
            _assetTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_assetTypeLabel, "Defines the asset type on which other properties depend.");
            // 
            // _soundKeyLabel
            // 
            _soundKeyLabel.Font = new Font("Segoe UI", 12F);
            _soundKeyLabel.Location = new Point(14, 152);
            _soundKeyLabel.Name = "_soundKeyLabel";
            _soundKeyLabel.Size = new Size(190, 34);
            _soundKeyLabel.TabIndex = 16;
            _soundKeyLabel.Text = "Sound Key:";
            _soundKeyLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_soundKeyLabel, "Specifies the localization key to get text data from the first subtitle.");
            // 
            // _soundTypeLabel
            // 
            _soundTypeLabel.Font = new Font("Segoe UI", 12F);
            _soundTypeLabel.Location = new Point(14, 62);
            _soundTypeLabel.Name = "_soundTypeLabel";
            _soundTypeLabel.Size = new Size(190, 36);
            _soundTypeLabel.TabIndex = 20;
            _soundTypeLabel.Text = "Sound Type:";
            _soundTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_soundTypeLabel, "This parameter defines how the game balances the SoundObject's volume.\r\nUsers can reduce or increase the volume of a sound using the Options menu.");
            // 
            // _volumeMultiplierLabel
            // 
            _volumeMultiplierLabel.Font = new Font("Segoe UI", 12F);
            _volumeMultiplierLabel.Location = new Point(14, 107);
            _volumeMultiplierLabel.Name = "_volumeMultiplierLabel";
            _volumeMultiplierLabel.Size = new Size(190, 34);
            _volumeMultiplierLabel.TabIndex = 22;
            _volumeMultiplierLabel.Text = "Volume Multiplier:";
            _volumeMultiplierLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_volumeMultiplierLabel, resources.GetString("_volumeMultiplierLabel.ToolTip"));
            // 
            // _subtitleDurationLabel
            // 
            _subtitleDurationLabel.Font = new Font("Segoe UI", 12F);
            _subtitleDurationLabel.Location = new Point(14, 276);
            _subtitleDurationLabel.Name = "_subtitleDurationLabel";
            _subtitleDurationLabel.Size = new Size(190, 34);
            _subtitleDurationLabel.TabIndex = 24;
            _subtitleDurationLabel.Text = "Subtitle Duration:";
            _subtitleDurationLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_subtitleDurationLabel, "Defines how long the subtitles will be shown in the game.\r\nThe value is specified in seconds.");
            // 
            // _subtitleColorLabel
            // 
            _subtitleColorLabel.Font = new Font("Segoe UI", 12F);
            _subtitleColorLabel.Location = new Point(14, 321);
            _subtitleColorLabel.Name = "_subtitleColorLabel";
            _subtitleColorLabel.Size = new Size(190, 34);
            _subtitleColorLabel.TabIndex = 26;
            _subtitleColorLabel.Text = "Subtitle Color:";
            _subtitleColorLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_subtitleColorLabel, "Defines the color of the subtitles in the RGBA format.\r\n");
            // 
            // _addToMemoryCheckBox
            // 
            _addToMemoryCheckBox.Location = new Point(377, 397);
            _addToMemoryCheckBox.Name = "_addToMemoryCheckBox";
            _addToMemoryCheckBox.Size = new Size(139, 30);
            _addToMemoryCheckBox.TabIndex = 29;
            _addToMemoryCheckBox.Text = "Add To Memory";
            _toolTipStandard.SetToolTip(_addToMemoryCheckBox, "Allows the game to keep the SoundObject in a limited memory pool.\r\nIf a MusicManager instance has midiCorrupted set to true,\r\nthere is a chance that the SoundObject might be played randomly.");
            _addToMemoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // _additionalSoundKeysButton
            // 
            _additionalSoundKeysButton.Location = new Point(14, 219);
            _additionalSoundKeysButton.Name = "_additionalSoundKeysButton";
            _additionalSoundKeysButton.Size = new Size(502, 49);
            _additionalSoundKeysButton.TabIndex = 19;
            _additionalSoundKeysButton.Text = "Manage Additional Sound Keys";
            _toolTipStandard.SetToolTip(_additionalSoundKeysButton, "Use to add more subtitle text if space runs out\r\nor you want to split the text into multiple timestamps.");
            _additionalSoundKeysButton.UseVisualStyleBackColor = true;
            // 
            // _encryptedCheckBox
            // 
            _encryptedCheckBox.Location = new Point(419, 192);
            _encryptedCheckBox.Name = "_encryptedCheckBox";
            _encryptedCheckBox.Size = new Size(97, 26);
            _encryptedCheckBox.TabIndex = 18;
            _encryptedCheckBox.Text = "Encrypted";
            _encryptedCheckBox.TextAlign = ContentAlignment.TopLeft;
            _toolTipStandard.SetToolTip(_encryptedCheckBox, resources.GetString("_encryptedCheckBox.ToolTip"));
            _encryptedCheckBox.UseVisualStyleBackColor = true;
            // 
            // _progressBar
            // 
            _progressBar.Location = new Point(12, 662);
            _progressBar.Name = "_progressBar";
            _progressBar.Size = new Size(734, 47);
            _progressBar.TabIndex = 9;
            // 
            // _startButton
            // 
            _startButton.Location = new Point(752, 662);
            _startButton.Name = "_startButton";
            _startButton.Size = new Size(370, 47);
            _startButton.TabIndex = 13;
            _startButton.Text = "Create/Modify JSONs";
            _startButton.UseVisualStyleBackColor = true;
            _startButton.Click += StartButton_Click;
            // 
            // _fileListView
            // 
            _fileListView.Columns.AddRange(new ColumnHeader[] { _fileHeader, _jsonHeader, _assetTypeHeader });
            _fileListView.Location = new Point(550, 12);
            _fileListView.Name = "_fileListView";
            _fileListView.Size = new Size(572, 443);
            _fileListView.TabIndex = 17;
            _fileListView.UseCompatibleStateImageBehavior = false;
            _fileListView.View = View.Details;
            _fileListView.ItemSelectionChanged += FileListView_ItemSelectionChanged;
            // 
            // _fileHeader
            // 
            _fileHeader.Text = "File Name";
            _fileHeader.Width = 320;
            // 
            // _jsonHeader
            // 
            _jsonHeader.Text = "JSON";
            _jsonHeader.TextAlign = HorizontalAlignment.Center;
            _jsonHeader.Width = 100;
            // 
            // _assetTypeHeader
            // 
            _assetTypeHeader.Text = "Asset Type";
            _assetTypeHeader.TextAlign = HorizontalAlignment.Center;
            _assetTypeHeader.Width = 125;
            // 
            // _removeSelectedButton
            // 
            _removeSelectedButton.Location = new Point(924, 539);
            _removeSelectedButton.Name = "_removeSelectedButton";
            _removeSelectedButton.Size = new Size(198, 42);
            _removeSelectedButton.TabIndex = 19;
            _removeSelectedButton.Text = "Remove Selected Files";
            _removeSelectedButton.UseVisualStyleBackColor = true;
            _removeSelectedButton.Click += RemoveSelectedButton_Click;
            // 
            // _selectAllButton
            // 
            _selectAllButton.Location = new Point(924, 491);
            _selectAllButton.Name = "_selectAllButton";
            _selectAllButton.Size = new Size(198, 42);
            _selectAllButton.TabIndex = 20;
            _selectAllButton.Text = "Select All";
            _selectAllButton.UseVisualStyleBackColor = true;
            _selectAllButton.Click += SelectAllButton_Click;
            // 
            // _addFilesButton
            // 
            _addFilesButton.Location = new Point(550, 539);
            _addFilesButton.Name = "_addFilesButton";
            _addFilesButton.Size = new Size(166, 42);
            _addFilesButton.TabIndex = 21;
            _addFilesButton.Text = "Add Files";
            _addFilesButton.UseVisualStyleBackColor = true;
            _addFilesButton.Click += AddFilesButton_Click;
            // 
            // _addFilesFromBufferButton
            // 
            _addFilesFromBufferButton.Location = new Point(722, 539);
            _addFilesFromBufferButton.Name = "_addFilesFromBufferButton";
            _addFilesFromBufferButton.Size = new Size(196, 42);
            _addFilesFromBufferButton.TabIndex = 22;
            _addFilesFromBufferButton.Text = "Add Files from Clipboard";
            _addFilesFromBufferButton.UseVisualStyleBackColor = true;
            _addFilesFromBufferButton.Click += AddFilesFromBufferButton_Click;
            // 
            // _tipLabel
            // 
            _tipLabel.Font = new Font("Segoe UI", 7F);
            _tipLabel.Location = new Point(550, 458);
            _tipLabel.Name = "_tipLabel";
            _tipLabel.Size = new Size(342, 30);
            _tipLabel.TabIndex = 18;
            _tipLabel.Text = "Tip: drag and drop files right into this window.\r\nTip 2: ctrl + left mouse allows you to select specific files.";
            // 
            // _applyChangesButton
            // 
            _applyChangesButton.Enabled = false;
            _applyChangesButton.Location = new Point(12, 587);
            _applyChangesButton.Name = "_applyChangesButton";
            _applyChangesButton.Size = new Size(532, 42);
            _applyChangesButton.TabIndex = 23;
            _applyChangesButton.Text = "Apply Changes";
            _applyChangesButton.UseVisualStyleBackColor = true;
            _applyChangesButton.Click += ApplyChangesButton_Click;
            // 
            // _viewingFileLabel
            // 
            _viewingFileLabel.Location = new Point(12, 637);
            _viewingFileLabel.Name = "_viewingFileLabel";
            _viewingFileLabel.Size = new Size(532, 22);
            _viewingFileLabel.TabIndex = 24;
            _viewingFileLabel.Text = "You are viewing:";
            _viewingFileLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _textureSpritePanel
            // 
            _textureSpritePanel.BorderStyle = BorderStyle.FixedSingle;
            _textureSpritePanel.Controls.Add(_sprTitleLabel);
            _textureSpritePanel.Controls.Add(_centerTextBox);
            _textureSpritePanel.Controls.Add(_centerLabel);
            _textureSpritePanel.Controls.Add(_pixelsPerUnitTextBox);
            _textureSpritePanel.Controls.Add(_pixelsPerUnitLabel);
            _textureSpritePanel.Controls.Add(_formatLabel);
            _textureSpritePanel.Controls.Add(_formatComboBox);
            _textureSpritePanel.Controls.Add(_texTitleLabel);
            _textureSpritePanel.Location = new Point(12, 12);
            _textureSpritePanel.Name = "_textureSpritePanel";
            _textureSpritePanel.Size = new Size(532, 443);
            _textureSpritePanel.TabIndex = 25;
            _textureSpritePanel.Visible = false;
            // 
            // _sprTitleLabel
            // 
            _sprTitleLabel.Font = new Font("Segoe UI", 18F);
            _sprTitleLabel.Location = new Point(14, 174);
            _sprTitleLabel.Name = "_sprTitleLabel";
            _sprTitleLabel.Size = new Size(502, 47);
            _sprTitleLabel.TabIndex = 24;
            _sprTitleLabel.Text = "Sprite Properties";
            _sprTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _centerTextBox
            // 
            _centerTextBox.Font = new Font("Segoe UI", 12F);
            _centerTextBox.Location = new Point(210, 271);
            _centerTextBox.Name = "_centerTextBox";
            _centerTextBox.Size = new Size(306, 34);
            _centerTextBox.TabIndex = 23;
            // 
            // _pixelsPerUnitTextBox
            // 
            _pixelsPerUnitTextBox.Font = new Font("Segoe UI", 12F);
            _pixelsPerUnitTextBox.Location = new Point(210, 226);
            _pixelsPerUnitTextBox.Name = "_pixelsPerUnitTextBox";
            _pixelsPerUnitTextBox.Size = new Size(306, 34);
            _pixelsPerUnitTextBox.TabIndex = 21;
            // 
            // _pixelsPerUnitLabel
            // 
            _pixelsPerUnitLabel.Font = new Font("Segoe UI", 12F);
            _pixelsPerUnitLabel.Location = new Point(14, 226);
            _pixelsPerUnitLabel.Name = "_pixelsPerUnitLabel";
            _pixelsPerUnitLabel.Size = new Size(190, 34);
            _pixelsPerUnitLabel.TabIndex = 18;
            _pixelsPerUnitLabel.Text = "Pixels Per Unit:";
            _pixelsPerUnitLabel.TextAlign = ContentAlignment.MiddleCenter;
            _toolTipStandard.SetToolTip(_pixelsPerUnitLabel, "Defines the number of pixels per unit for the sprite.");
            // 
            // _formatComboBox
            // 
            _formatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _formatComboBox.Font = new Font("Segoe UI", 12F);
            _formatComboBox.FormattingEnabled = true;
            _formatComboBox.Location = new Point(210, 130);
            _formatComboBox.Name = "_formatComboBox";
            _formatComboBox.Size = new Size(306, 36);
            _formatComboBox.TabIndex = 14;
            // 
            // _texTitleLabel
            // 
            _texTitleLabel.Font = new Font("Segoe UI", 18F);
            _texTitleLabel.Location = new Point(14, 75);
            _texTitleLabel.Name = "_texTitleLabel";
            _texTitleLabel.Size = new Size(502, 47);
            _texTitleLabel.TabIndex = 13;
            _texTitleLabel.Text = "Texture2D Properties";
            _texTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _assetNameTextBox
            // 
            _assetNameTextBox.Font = new Font("Segoe UI", 12F);
            _assetNameTextBox.Location = new Point(210, 62);
            _assetNameTextBox.Name = "_assetNameTextBox";
            _assetNameTextBox.PlaceholderText = "Optional";
            _assetNameTextBox.Size = new Size(306, 34);
            _assetNameTextBox.TabIndex = 20;
            // 
            // _soundPanel
            // 
            _soundPanel.BorderStyle = BorderStyle.FixedSingle;
            _soundPanel.Controls.Add(_soundPaletteButton);
            _soundPanel.Controls.Add(_soundTypeComboBox);
            _soundPanel.Controls.Add(_addToMemoryCheckBox);
            _soundPanel.Controls.Add(_subtitleDurationTextBox);
            _soundPanel.Controls.Add(_subtitleColorLabel);
            _soundPanel.Controls.Add(_subtitleColorTextBox);
            _soundPanel.Controls.Add(_subtitleDurationLabel);
            _soundPanel.Controls.Add(_volumeTextBox);
            _soundPanel.Controls.Add(_volumeMultiplierLabel);
            _soundPanel.Controls.Add(_soundTypeLabel);
            _soundPanel.Controls.Add(_additionalSoundKeysButton);
            _soundPanel.Controls.Add(_encryptedCheckBox);
            _soundPanel.Controls.Add(_soundKeyTextBox);
            _soundPanel.Controls.Add(_soundKeyLabel);
            _soundPanel.Controls.Add(_soundObjectTitleLabel);
            _soundPanel.Location = new Point(12, 12);
            _soundPanel.Name = "_soundPanel";
            _soundPanel.Size = new Size(532, 443);
            _soundPanel.TabIndex = 24;
            _soundPanel.Visible = false;
            // 
            // _soundPaletteButton
            // 
            _soundPaletteButton.Location = new Point(395, 321);
            _soundPaletteButton.Name = "_soundPaletteButton";
            _soundPaletteButton.Size = new Size(121, 34);
            _soundPaletteButton.TabIndex = 31;
            _soundPaletteButton.Text = "Palette";
            _soundPaletteButton.UseVisualStyleBackColor = true;
            _soundPaletteButton.Click += SoundPaletteButton_Click;
            // 
            // _soundTypeComboBox
            // 
            _soundTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _soundTypeComboBox.Font = new Font("Segoe UI", 12F);
            _soundTypeComboBox.FormattingEnabled = true;
            _soundTypeComboBox.Items.AddRange(new object[] { "Effect", "Music", "Voice" });
            _soundTypeComboBox.Location = new Point(210, 62);
            _soundTypeComboBox.Name = "_soundTypeComboBox";
            _soundTypeComboBox.Size = new Size(306, 36);
            _soundTypeComboBox.TabIndex = 30;
            // 
            // _subtitleDurationTextBox
            // 
            _subtitleDurationTextBox.Font = new Font("Segoe UI", 12F);
            _subtitleDurationTextBox.Location = new Point(210, 276);
            _subtitleDurationTextBox.Name = "_subtitleDurationTextBox";
            _subtitleDurationTextBox.Size = new Size(306, 34);
            _subtitleDurationTextBox.TabIndex = 27;
            // 
            // _subtitleColorTextBox
            // 
            _subtitleColorTextBox.Font = new Font("Segoe UI", 12F);
            _subtitleColorTextBox.Location = new Point(210, 321);
            _subtitleColorTextBox.Name = "_subtitleColorTextBox";
            _subtitleColorTextBox.Size = new Size(179, 34);
            _subtitleColorTextBox.TabIndex = 25;
            // 
            // _volumeTextBox
            // 
            _volumeTextBox.Font = new Font("Segoe UI", 12F);
            _volumeTextBox.Location = new Point(210, 107);
            _volumeTextBox.Name = "_volumeTextBox";
            _volumeTextBox.Size = new Size(306, 34);
            _volumeTextBox.TabIndex = 23;
            // 
            // _soundKeyTextBox
            // 
            _soundKeyTextBox.Font = new Font("Segoe UI", 12F);
            _soundKeyTextBox.Location = new Point(210, 152);
            _soundKeyTextBox.Name = "_soundKeyTextBox";
            _soundKeyTextBox.Size = new Size(306, 34);
            _soundKeyTextBox.TabIndex = 17;
            // 
            // _soundObjectTitleLabel
            // 
            _soundObjectTitleLabel.Font = new Font("Segoe UI", 18F);
            _soundObjectTitleLabel.Location = new Point(14, 10);
            _soundObjectTitleLabel.Name = "_soundObjectTitleLabel";
            _soundObjectTitleLabel.Size = new Size(502, 47);
            _soundObjectTitleLabel.TabIndex = 14;
            _soundObjectTitleLabel.Text = "SoundObject Properties";
            _soundObjectTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _assetBasicInfoPanel
            // 
            _assetBasicInfoPanel.BorderStyle = BorderStyle.FixedSingle;
            _assetBasicInfoPanel.Controls.Add(_assetTypeComboBox);
            _assetBasicInfoPanel.Controls.Add(_assetTypeLabel);
            _assetBasicInfoPanel.Controls.Add(_assetNameLabel);
            _assetBasicInfoPanel.Controls.Add(_assetNameTextBox);
            _assetBasicInfoPanel.Location = new Point(12, 461);
            _assetBasicInfoPanel.Name = "_assetBasicInfoPanel";
            _assetBasicInfoPanel.Size = new Size(532, 120);
            _assetBasicInfoPanel.TabIndex = 26;
            // 
            // _assetTypeComboBox
            // 
            _assetTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            _assetTypeComboBox.Font = new Font("Segoe UI", 12F);
            _assetTypeComboBox.FormattingEnabled = true;
            _assetTypeComboBox.Items.AddRange(new object[] { "None", "Texture2D", "Sprite", "Spritesheet", "AudioClip", "SoundObject" });
            _assetTypeComboBox.Location = new Point(210, 15);
            _assetTypeComboBox.Name = "_assetTypeComboBox";
            _assetTypeComboBox.Size = new Size(306, 36);
            _assetTypeComboBox.TabIndex = 25;
            // 
            // _addFilesFromFolderButton
            // 
            _addFilesFromFolderButton.Location = new Point(550, 491);
            _addFilesFromFolderButton.Name = "_addFilesFromFolderButton";
            _addFilesFromFolderButton.Size = new Size(368, 42);
            _addFilesFromFolderButton.TabIndex = 27;
            _addFilesFromFolderButton.Text = "Add Files from Folder";
            _addFilesFromFolderButton.UseVisualStyleBackColor = true;
            _addFilesFromFolderButton.Click += AddFilesFromFolderButton_Click;
            // 
            // _noPropertiesPanel
            // 
            _noPropertiesPanel.BorderStyle = BorderStyle.FixedSingle;
            _noPropertiesPanel.Location = new Point(12, 12);
            _noPropertiesPanel.Name = "_noPropertiesPanel";
            _noPropertiesPanel.Size = new Size(532, 443);
            _noPropertiesPanel.TabIndex = 31;
            // 
            // _deleteSelectedJsonsButton
            // 
            _deleteSelectedJsonsButton.Location = new Point(924, 587);
            _deleteSelectedJsonsButton.Name = "_deleteSelectedJsonsButton";
            _deleteSelectedJsonsButton.Size = new Size(198, 42);
            _deleteSelectedJsonsButton.TabIndex = 32;
            _deleteSelectedJsonsButton.Text = "Delete Selected JSON Files";
            _deleteSelectedJsonsButton.UseVisualStyleBackColor = true;
            _deleteSelectedJsonsButton.Click += DeleteSelectedJsonsButton_Click;
            // 
            // JsonAssetEditorForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 721);
            Controls.Add(_deleteSelectedJsonsButton);
            Controls.Add(_addFilesFromFolderButton);
            Controls.Add(_assetBasicInfoPanel);
            Controls.Add(_viewingFileLabel);
            Controls.Add(_applyChangesButton);
            Controls.Add(_addFilesFromBufferButton);
            Controls.Add(_addFilesButton);
            Controls.Add(_selectAllButton);
            Controls.Add(_removeSelectedButton);
            Controls.Add(_tipLabel);
            Controls.Add(_fileListView);
            Controls.Add(_startButton);
            Controls.Add(_progressBar);
            Controls.Add(_soundPanel);
            Controls.Add(_textureSpritePanel);
            Controls.Add(_noPropertiesPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "JsonAssetEditorForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "JSON Asset Editor";
            DragDrop += TextureAssetForm_DragDrop;
            DragEnter += TextureAssetForm_DragEnter;
            _textureSpritePanel.ResumeLayout(false);
            _textureSpritePanel.PerformLayout();
            _soundPanel.ResumeLayout(false);
            _soundPanel.PerformLayout();
            _assetBasicInfoPanel.ResumeLayout(false);
            _assetBasicInfoPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        internal ToolTip _toolTipStandard;
        internal ProgressBar _progressBar;
        internal Button _startButton;
        internal ListView _fileListView;
        internal ColumnHeader _fileHeader;
        internal Button _removeSelectedButton;
        internal Button _selectAllButton;
        internal Button _addFilesButton;
        internal Button _addFilesFromBufferButton;
        internal Label _tipLabel;
        internal Button _applyChangesButton;
        internal Label _viewingFileLabel;
        internal ColumnHeader _jsonHeader;
        internal Panel _textureSpritePanel;
        internal TextBox _centerTextBox;
        internal Label _centerLabel;
        internal TextBox _pixelsPerUnitTextBox;
        internal TextBox _assetNameTextBox;
        internal Label _assetNameLabel;
        internal Label _pixelsPerUnitLabel;
        internal Label _formatLabel;
        internal ComboBox _formatComboBox;
        internal Label _texTitleLabel;
        internal Panel _soundPanel;
        internal Panel _assetBasicInfoPanel;
        internal Label _sprTitleLabel;
        internal ComboBox _assetTypeComboBox;
        internal Label _assetTypeLabel;
        internal Label _soundObjectTitleLabel;
        internal Label _soundKeyLabel;
        internal TextBox _soundKeyTextBox;
        internal CheckBox _encryptedCheckBox;
        internal Button _additionalSoundKeysButton;
        internal Label _soundTypeLabel;
        internal TextBox _volumeTextBox;
        internal Label _volumeMultiplierLabel;
        internal TextBox _subtitleColorTextBox;
        internal Label _subtitleDurationLabel;
        internal TextBox _subtitleDurationTextBox;
        internal Label _subtitleColorLabel;
        internal CheckBox _addToMemoryCheckBox;
        internal ComboBox _soundTypeComboBox;
        internal Button _addFilesFromFolderButton;
        private Panel _noPropertiesPanel;
        private ColumnHeader _assetTypeHeader;
        private Button _soundPaletteButton;
        private ColorDialog _colorDialog;
        private Button _deleteSelectedJsonsButton;
    }
}