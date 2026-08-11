using NineNineFrameworkTool.Data;
using NineNineFrameworkTool.Utils;

namespace NineNineFrameworkTool.Forms
{
    internal partial class MainForm : Form
    {
        public static string[] SupportedAssetExtensions { get; private set; } =
            ["png", "jpg", "jpeg"];

        public MainForm()
        {
            this.FormClosing += OnFormClosing;
            InitializeComponent();

            _notifyIconCheckBox.Checked = _notifyIcon.Visible = AppConfigDataManager.Data.NotifyIcon;

            _notifyIcon.MouseClick += NotifyIcon_MouseClick;
            _notifyIconCheckBox.CheckedChanged += NotifyIconCheckBox_CheckedChanged;
        }

        private void OpenForm(Form form)
        {
            form.Show();
            //WindowState = FormWindowState.Minimized;
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs args)
        {
            if (!_notifyIcon.Visible || args.CloseReason != CloseReason.UserClosing)
            {
                Application.Exit();
            }
            else
            {
                args.Cancel = true;
                Visible = false;
            }
        }

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                if (Visible)
                {
                    WindowState = FormWindowState.Normal;
                    Activate();
                }
            }
        }

        private void TextureSpriteButton_Click(object? sender, EventArgs e) => OpenForm(new JsonAssetEditorForm());

        private void TextEncryptorButton_Click(object? sender, EventArgs e) => OpenForm(new RijndaelEncryptorForm());

        private void ColorConverterButton_Click(object sender, EventArgs e) => OpenForm(new UnityColorConverterForm());

        private void NotifyIconCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            _notifyIcon.Visible = AppConfigDataManager.Data.NotifyIcon = _notifyIconCheckBox.Checked;
            AppConfigDataManager.Save();
        }

        private void CloseAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void JsonAssetToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            OpenForm(new JsonAssetEditorForm());
        }

        private void LocalizationEncryptorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new RijndaelEncryptorForm());
        }

        private void ColorNormalizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new UnityColorConverterForm());
        }
    }
}