using NineNineFrameworkTool.Utils;

namespace NineNineFrameworkTool.Forms
{
    internal partial class RijndaelEncryptorForm : Form
    {
        private const string GameEncryptionKey = "baldi";

        private RijndaelEncryption _rijndael = new RijndaelEncryption();

        public RijndaelEncryptorForm()
        {
            InitializeComponent();
            _modeComboBox.SelectedIndex = 0;
        }

        private void CopyToBufferButton_Click(object sender, EventArgs e) => Clipboard.SetText(_outputRichTextBox.Text);

        private void HandleButton_Click(object sender, EventArgs e)
        {
            if (_modeComboBox.Text == "Encrypt")
            {
                _outputRichTextBox.Text = _rijndael.Encrypt(_inputRichTextBox.Text, GameEncryptionKey);
            }
            else
            {
                _outputRichTextBox.Text = _rijndael.Decrypt(_inputRichTextBox.Text, GameEncryptionKey);
            }
        }
    }
}
