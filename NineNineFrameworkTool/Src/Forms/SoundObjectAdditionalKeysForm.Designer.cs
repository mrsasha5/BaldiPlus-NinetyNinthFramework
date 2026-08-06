namespace NineNineFrameworkTool.Forms
{
    partial class SoundObjectAdditionalKeysForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundObjectAdditionalKeysForm));
            _dataGridView = new DataGridView();
            _keyColumn = new DataGridViewTextBoxColumn();
            _timeColumn = new DataGridViewTextBoxColumn();
            _encryptedColumn = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // _dataGridView
            // 
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new DataGridViewColumn[] { _keyColumn, _timeColumn, _encryptedColumn });
            _dataGridView.Location = new Point(12, 12);
            _dataGridView.Name = "_dataGridView";
            _dataGridView.RowHeadersWidth = 51;
            _dataGridView.Size = new Size(776, 426);
            _dataGridView.TabIndex = 0;
            // 
            // _keyColumn
            // 
            _keyColumn.HeaderText = "Localization Key";
            _keyColumn.MinimumWidth = 6;
            _keyColumn.Name = "_keyColumn";
            _keyColumn.Width = 250;
            // 
            // _timeColumn
            // 
            _timeColumn.HeaderText = "Time";
            _timeColumn.MinimumWidth = 6;
            _timeColumn.Name = "_timeColumn";
            _timeColumn.Width = 125;
            // 
            // _encryptedColumn
            // 
            _encryptedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _encryptedColumn.HeaderText = "Encrypted";
            _encryptedColumn.MinimumWidth = 6;
            _encryptedColumn.Name = "_encryptedColumn";
            // 
            // SoundObjectAdditionalKeysForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(_dataGridView);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SoundObjectAdditionalKeysForm";
            Text = "SoundObject: Additional Keys";
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView _dataGridView;
        private DataGridViewTextBoxColumn _keyColumn;
        private DataGridViewTextBoxColumn _timeColumn;
        private DataGridViewCheckBoxColumn _encryptedColumn;
    }
}