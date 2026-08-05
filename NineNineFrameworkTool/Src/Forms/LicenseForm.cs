using NineNineFrameworkTool.Data;

namespace NineNineFrameworkTool.Forms
{
    internal partial class LicenseForm : Form
    {
        public LicenseForm()
        {
            InitializeComponent();
            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object? sender, FormClosedEventArgs args)
        {
            Application.Exit();
        }

        private void LicenseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _continueButton.Enabled = _licenseCheckBox.Checked;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            AppConfigDataManager.Init();
            AppConfigDataManager.Save();
            FormClosed -= OnFormClosed;
            Close();
            Program.MainForm = new MainForm();
            Program.MainForm.Show();
        }
    }
}
