using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace NineNineFrameworkTool.Forms
{
    internal partial class SoundObjectAdditionalKeysForm : Form
    {
        public List<SoundObjectJsonAssetData.SubtitleTimedKey> TimedKeys { get; set; } = new();

        public SoundObjectAdditionalKeysForm()
        {
            InitializeComponent();
            FormClosing += SoundObjectAdditionalKeysForm_FormClosing;
        }

        private void SoundObjectAdditionalKeysForm_FormClosing(object? sender, FormClosingEventArgs args)
        {
            if (args.CloseReason == CloseReason.UserClosing)
            {
                args.Cancel = true;
                Visible = false;
            }
        }

        public void UpdateTable()
        {
            _dataGridView.CellValueChanged -= DataGridView_CellValueChanged;
            _dataGridView.Rows.Clear();
            foreach (SoundObjectJsonAssetData.SubtitleTimedKey timedKey in TimedKeys)
            {
                _dataGridView.Rows.Add(timedKey.Key, timedKey.Time.ToString(CultureInfo.InvariantCulture.NumberFormat), 
                    timedKey.Encrypted);
            }
            _dataGridView.CellValueChanged += DataGridView_CellValueChanged;
        }

        private void DataGridView_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            TimedKeys.Clear();
            for (int i = 0; i < _dataGridView.Rows.Count - 1; i++) // Skip the last row to prevent from creating duplicates
            {
                DataGridViewRow row = _dataGridView.Rows[i];
                SoundObjectJsonAssetData.SubtitleTimedKey key = new();
                key.Key = (string)row.Cells[0].Value;
                if (row.Cells[1].Value != null && float.TryParse(((string)row.Cells[1].Value).Replace(',', '.'), 
                    CultureInfo.InvariantCulture.NumberFormat, out float value)) key.Time = value;
                key.Encrypted = row.Cells[2].Value != null ? ((bool)row.Cells[2].Value) : false;
                TimedKeys.Add(key);
            }
            UpdateTable();
        }
    }
}