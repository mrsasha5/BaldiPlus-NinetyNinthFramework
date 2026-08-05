using NineNineFrameworkTool.Unity;
using NineNineFrameworkTool.Utils;
using System.Collections.Specialized;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace NineNineFrameworkTool.Forms
{
    internal partial class JsonAssetEditorForm : Form
    {
        private List<ListViewItem> _selectedItems = new List<ListViewItem>();

        private Dictionary<ListViewItem, JsonAssetData> _loadedFiles = new Dictionary<ListViewItem, JsonAssetData>();

        internal static JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            IncludeFields = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
            {
                Modifiers = { TextureJsonAssetData.RemoveUnusedProperties }
            },
            WriteIndented = true
        };

        private void TextureAssetForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TextureAssetForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data?.GetData(DataFormats.FileDrop)!;
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    AddPath(files[i]);
                }
            }
        }

        public JsonAssetEditorForm()
        {
            InitializeComponent();
            foreach (TextureFormat format in Enum.GetValues<TextureFormat>())
            {
                _formatComboBox.Items.Add(format.ToString());
            }
            CleanBaseProperties();
        }

        private void HandleJsons()
        {
            _progressBar.Minimum = 0;
            _progressBar.Maximum = _loadedFiles.Values.Count;
            int counter = 0;
            foreach (JsonAssetData data in _loadedFiles.Values)
            {
                if (data.AssetType == AssetType.None) continue;

                using (FileStream stream = new FileStream(data.Path.ReplaceExtensionPath("json"), FileMode.Create))
                {
                    switch (data.AssetType)
                    {
                        case AssetType.Texture2D:
                            JsonSerializer.Serialize(stream, (TextureJsonAssetData)data, s_jsonSerializerOptions);
                            break;
                        case AssetType.Sprite:
                            JsonSerializer.Serialize(stream, (TextureJsonAssetData)data, s_jsonSerializerOptions);
                            break;
                        case AssetType.SoundObject:
                            JsonSerializer.Serialize(stream, (SoundObjectJsonAssetData)data, s_jsonSerializerOptions);
                            break;
                    }

                    stream.Flush();
                    counter++;
                    _progressBar.Invoke(delegate ()
                    {
                        // This hack allows to disable smooth animation which was a reason of impossibility to see the progress in real time.
                        // Also condition exists since setting value higher than max will throw an exception
                        if (counter + 1 <= _progressBar.Maximum) _progressBar.Value = counter + 1;
                        _progressBar.Value = counter;

                        data.LoadStatus = JsonLoadStatus.Successful;
                        data.UpdateUI();
                    });
                }
            }
            Invoke(delegate ()
            {
                _progressBar.Value = 0;
                EnableFileManagement(true);
                UpdateProperties();
                _startButton.Enabled = true;
            });
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            _startButton.Enabled = false;
            EnableFileManagement(false);
            EnableProperties(false);
            Task.Run(HandleJsons);
        }

        public void AddPath(string path)
        {
            string extension = Path.GetExtension(path);
            extension = extension.Substring(1, extension.Length - 1);

            if (!MainForm.SupportedAssetExtensions.Contains(extension))
            {
                return;
            }

            foreach (JsonAssetData data in _loadedFiles.Values)
            {
                // Since Windows file system is case-insensitive
                if (string.Equals(Path.GetFullPath(data.Path), Path.GetFullPath(path), StringComparison.OrdinalIgnoreCase)) return;
            }

            ListViewItem item = _fileListView.Items.Add(string.Empty);
            JsonAssetData jsonData = JsonAssetData.Load(path);
            jsonData.UIItem = item;
            jsonData.UpdateUI();
            _loadedFiles.Add(item, jsonData);
        }

        private void AddFilesFromBufferButton_Click(object sender, EventArgs e)
        {
            StringCollection files = Clipboard.GetFileDropList();
            for (int i = 0; i < files.Count; i++)
            {
                AddPath(files[i]!);
            }
        }

        private void AddFilesFromFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in Directory.GetFiles(folderDialog.SelectedPath, "*", SearchOption.AllDirectories))
                {
                    AddPath(path);
                }
            }
        }

        private void AddFilesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "Select Files To Add";
            fileDialog.AddExtension = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in fileDialog.FileNames)
                {
                    AddPath(path);
                }
            }
        }

        private void RemoveSelectedButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _fileListView.Items.Count; i++)
            {
                if (_fileListView.Items[i].Selected)
                {
                    _loadedFiles.Remove(_fileListView.Items[i]);
                    _fileListView.Items.RemoveAt(i);
                    i--;
                }
            }
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            bool unselect = true;
            for (int i = 0; i < _fileListView.Items.Count; i++)
            {
                if (!_fileListView.Items[i].Selected)
                {
                    unselect = false;
                    break;
                }
            }
            if (!unselect) _fileListView.Focus(); // Yes, blue color!
            for (int i = 0; i < _fileListView.Items.Count; i++)
            {
                _fileListView.Items[i].Selected = !unselect;
            }
        }

        private void FileListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _selectedItems.Add(e.Item!);
            }
            else
            {
                _selectedItems.Remove(e.Item!);
            }

            UpdateProperties();

            if (_loadedFiles.Count != _selectedItems.Count)
            {
                _selectAllButton.Text = "Select All";
            }
            else _selectAllButton.Text = "Unselect All";
        }

        private void ApplyChangesButton_Click(object sender, EventArgs e)
        {
            ApplyPropertiesToSelectedItems();
            UpdateProperties();
        }

        private void ApplyPropertiesToSelectedItems()
        {
            AssetType type = GetAssetType();

            for (int i = 0; i < _selectedItems.Count; i++)
            {
                JsonAssetData data = _loadedFiles[_selectedItems[i]];

                if (data.AssetType != type)
                {
                    data = _loadedFiles[_selectedItems[i]] = JsonAssetData.InitAssetOfType(type, data.Path, data.UIItem, data.LoadStatus);
                }

                data.ApplyProperties(this);
                data.UpdateUI();
            }
        }

        private void UpdateProperties()
        {
            if (_selectedItems.Count == 1)
            {
                JsonAssetData data = _loadedFiles[_selectedItems[0]];
                if (data.LoadStatus == JsonLoadStatus.Failed)
                {
                    _viewingFileLabel.Text = $"Loaded JSON cannot be handled.";
                }
                else
                {
                    EnableBaseProperties(true);
                    LoadProperties(data);

                    string fileName = Path.GetFileName(data.Path);
                    /*if (fileName.Length >= 22)
                    {
                        fileName = $"{fileName.Substring(0, 19)}...";
                    }*/
                    _viewingFileLabel.Text = $"You are viewing: {fileName}";

                    _applyChangesButton.Enabled = true;
                }
            }
            else if (_selectedItems.Count == 0)
            {
                _applyChangesButton.Enabled = false;
                _textureSpritePanel.Visible = _soundPanel.Visible = false;
                EnableBaseProperties(false);
                CleanBaseProperties();

                _viewingFileLabel.Text = "You are viewing: no file(s) selected.";
            }
            else
            {
                // Check if properties of all selected files are same
                // If so, then display them
                // Otherwise clean
                // But allow to override in both cases

                bool same = true;
                for (int i = 0; i < _selectedItems.Count - 1; i++)
                {
                    JsonAssetData data1 = _loadedFiles[_selectedItems[i]];
                    JsonAssetData data2 = _loadedFiles[_selectedItems[i + 1]];

                    if (!data2.IsSame(data1))
                    {
                        same = false;
                        break;
                    }
                }

                EnableBaseProperties(true);

                if (same)
                {
                    LoadProperties(_loadedFiles[_selectedItems[0]]);
                    _viewingFileLabel.Text = "You are viewing: properties of all selected files.";
                }
                else
                {
                    _textureSpritePanel.Visible = _soundPanel.Visible = false;
                    _viewingFileLabel.Text = "Properties cannot be viewed. They are not same.";
                }

                _applyChangesButton.Enabled = true;
            }
        }

        private AssetType GetAssetType()
        {
            if (Enum.TryParse(_assetTypeComboBox.Text, out AssetType type))
            {
                return type;
            }
            else
            {
                return AssetType.None;
            }
        }

        private void LoadProperties(JsonAssetData data)
        {
            EnableProperties(true);
            data.LoadProperties(this);
        }

        private void EnableFileManagement(bool value)
        {
            _addFilesButton.Enabled = value;
            _addFilesFromBufferButton.Enabled = value;
            _selectAllButton.Enabled = value;
            _removeSelectedButton.Enabled = value;
            _fileListView.Enabled = value;
        }

        private void CleanBaseProperties()
        {
            _assetTypeComboBox.Text = AssetType.None.ToString();
            _assetNameTextBox.Text = string.Empty;
        }

        private void EnableBaseProperties(bool value)
        {
            _assetNameTextBox.Enabled = value;
            _assetTypeComboBox.Enabled = value;
        }

        private void EnableProperties(bool value)
        {
            _textureSpritePanel.Enabled = value;
            _soundPanel.Enabled = value;
        }

        private void EnableSpriteProperties(bool value)
        {
            _pixelsPerUnitTextBox.Enabled = value;
            _centerTextBox.Enabled = value;
        }

        private void CleanProperties()
        {
            // General data
            _assetTypeComboBox.Text = string.Empty;
            _assetNameTextBox.Text = string.Empty;

            if (_textureSpritePanel.Visible)
            {
                // Texture2D
                _formatComboBox.Text = string.Empty;

                // Sprite
                _pixelsPerUnitTextBox.Text = string.Empty;
                _centerTextBox.Text = string.Empty;
            }
            else if (_soundPanel.Visible)
            {
                // Sound Object
                _soundTypeComboBox.Text = string.Empty;
                _soundKeyTextBox.Text = string.Empty;
                _encryptedCheckBox.Checked = false;
                _volumeTextBox.Text = string.Empty;
                _subtitleDurationTextBox.Text = string.Empty;
                _subtitleColorTextBox.Text = string.Empty;
                _hasAnimationCheckBox.Checked = false;
                _addToMemoryCheckBox.Checked = false;
            }
        }

        private void SoundPaletteButton_Click(object sender, EventArgs e)
        {
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _subtitleColorTextBox.Text = Unity.Color.GetFromSystemColor(_colorDialog.Color).GetAsStringNoPercents();
            }
        }
    }
}
