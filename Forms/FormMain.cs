using Pokemon_Quest_Text_Decoder.Data;

namespace Pokemon_Quest_Text_Decoder;

public partial class FormMain : Form
{
    private MessageData data = new MessageData();

    public FormMain()
    {
        InitializeComponent();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            data.Decode(File.ReadAllBytes(openFileDialog.FileName));
        }
    }

    private void btnSaveTxt_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(saveFileDialog.FileName, data.ExportText());
            MessageBox.Show("Successfully exported the text!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnSaveBin_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllBytes(saveFileDialog.FileName, data.Encode(MessageData.Coded.DATA_NO_CODED));
            MessageBox.Show("Successfully exported the re-encoded text!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnTest_Click(object sender, EventArgs e)
    {

    }
}
