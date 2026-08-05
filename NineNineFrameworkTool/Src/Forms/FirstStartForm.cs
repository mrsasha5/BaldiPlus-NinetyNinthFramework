namespace NineNineFrameworkTool.Forms
{
    internal partial class FirstStartForm : Form
    {
        public FirstStartForm()
        {
            InitializeComponent();
            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object? sender, FormClosedEventArgs args)
        {
            Application.Exit(); 
        }

        private void ReadLicenseButton_Click(object? sender, EventArgs args)
        {
            FormClosed -= OnFormClosed;
            Close();
            new LicenseForm().Show();
        }
    }
}
