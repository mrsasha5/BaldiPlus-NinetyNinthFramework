namespace NineNineFrameworkTool.Forms
{
    public partial class UnityColorConverterForm : Form
    {
        public UnityColorConverterForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Unity.Color? color = Unity.Color.GetFromStringNoPercents(_inputTextBox.Text);
            if (color != null)
            {
                _outputTextBox.Text = color.Value.GetAsString();
            }
        }

        private void PaletteButton_Click(object sender, EventArgs e)
        {
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _inputTextBox.Text = Unity.Color.GetFromSystemColor(_colorDialog.Color).GetAsStringNoPercents();
            }
        }
    }
}