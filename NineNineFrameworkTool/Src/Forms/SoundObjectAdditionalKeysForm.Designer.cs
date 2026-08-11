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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundObjectAdditionalKeysForm));
            _dataGridView = new DataGridView();
            _keyColumn = new DataGridViewTextBoxColumn();
            _timeColumn = new DataGridViewTextBoxColumn();
            _encryptedColumn = new DataGridViewCheckBoxColumn();
            _rowContextMenuStrip = new ContextMenuStrip(components);
            deleteToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toEndToolStripMenuItem = new ToolStripMenuItem();
            moveDownToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toEndToolStripMenuItem1 = new ToolStripMenuItem();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            _rowContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _dataGridView
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            _dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new DataGridViewColumn[] { _keyColumn, _timeColumn, _encryptedColumn });
            _dataGridView.ContextMenuStrip = _rowContextMenuStrip;
            _dataGridView.Location = new Point(12, 12);
            _dataGridView.Name = "_dataGridView";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            _dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            _dataGridView.RowHeadersWidth = 51;
            _dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            _dataGridView.Size = new Size(776, 426);
            _dataGridView.TabIndex = 0;
            _dataGridView.CellValueChanged += DataGridView_CellValueChanged;
            // 
            // _keyColumn
            // 
            _keyColumn.HeaderText = "Localization Key";
            _keyColumn.MinimumWidth = 6;
            _keyColumn.Name = "_keyColumn";
            _keyColumn.Width = 400;
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
            // _rowContextMenuStrip
            // 
            _rowContextMenuStrip.ImageScalingSize = new Size(20, 20);
            _rowContextMenuStrip.Items.AddRange(new ToolStripItem[] { deleteToolStripMenuItem, moveDownToolStripMenuItem, deleteToolStripMenuItem1 });
            _rowContextMenuStrip.Name = "_rowContextMenuStrip";
            _rowContextMenuStrip.Size = new Size(159, 76);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem5, toolStripMenuItem6, toEndToolStripMenuItem });
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(158, 24);
            deleteToolStripMenuItem.Text = "Move Up";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(137, 26);
            toolStripMenuItem2.Text = "+1";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(137, 26);
            toolStripMenuItem3.Text = "+2";
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(137, 26);
            toolStripMenuItem5.Text = "+5";
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(137, 26);
            toolStripMenuItem6.Text = "+10";
            // 
            // toEndToolStripMenuItem
            // 
            toEndToolStripMenuItem.Name = "toEndToolStripMenuItem";
            toEndToolStripMenuItem.Size = new Size(137, 26);
            toEndToolStripMenuItem.Text = "To End";
            // 
            // moveDownToolStripMenuItem
            // 
            moveDownToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem10, toolStripMenuItem11, toEndToolStripMenuItem1 });
            moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            moveDownToolStripMenuItem.Size = new Size(158, 24);
            moveDownToolStripMenuItem.Text = "Move Down";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(137, 26);
            toolStripMenuItem7.Text = "+1";
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(137, 26);
            toolStripMenuItem8.Text = "+2";
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(137, 26);
            toolStripMenuItem10.Text = "+5";
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(137, 26);
            toolStripMenuItem11.Text = "+10";
            // 
            // toEndToolStripMenuItem1
            // 
            toEndToolStripMenuItem1.Name = "toEndToolStripMenuItem1";
            toEndToolStripMenuItem1.Size = new Size(137, 26);
            toEndToolStripMenuItem1.Text = "To End";
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(158, 24);
            deleteToolStripMenuItem1.Text = "Delete";
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
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SoundObject: Additional Keys";
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            _rowContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView _dataGridView;
        private ContextMenuStrip _rowContextMenuStrip;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem moveDownToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toEndToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem toEndToolStripMenuItem1;
        private DataGridViewTextBoxColumn _keyColumn;
        private DataGridViewTextBoxColumn _timeColumn;
        private DataGridViewCheckBoxColumn _encryptedColumn;
    }
}