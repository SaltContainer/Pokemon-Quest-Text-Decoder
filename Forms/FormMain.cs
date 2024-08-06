using Pokemon_Quest_Text_Decoder.Data;
using Pokemon_Quest_Text_Decoder.Engine;

namespace Pokemon_Quest_Text_Decoder;

public partial class FormMain : Form
{
    private MessageData data;

    private MessageDataEngine engine;

    public FormMain()
    {
        InitializeComponent();
        ToggleComponents(false);

        engine = new MessageDataEngine();
    }

    private void ToggleComponents(bool value)
    {
        rtxtLabel.Enabled = value;
        btnSaveLabel.Enabled = value;
        numUserParam.Enabled = value;
    }

    private void BindListBox()
    {
        listLabels.DataSource = data[0];
    }

    private void UpdateEditingData(int selectedIndex)
    {
        var param = data.GetStringParams(0)[selectedIndex];
        rtxtLabel.Text = data[0, selectedIndex];
        numUserParam.Value = param.userParam;
    }

    private void UpdateListBox()
    {
        BindListBox();
        UpdateEditingData(listLabels.SelectedIndex);
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            data = engine.ReadMessageDataFromBinFile(openFileDialog.FileName);
            UpdateListBox();
            ToggleComponents(true);
        }
    }

    private void btnSaveTxt_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            engine.SaveMessageDataToTextFile(saveFileDialog.FileName, data);
            MessageBox.Show("Successfully exported the text!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnSaveBin_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            engine.SaveMessageDataToBinFile(saveFileDialog.FileName, data);
            MessageBox.Show("Successfully exported the re-encoded text!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void listLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateEditingData(listLabels.SelectedIndex);
    }
}
