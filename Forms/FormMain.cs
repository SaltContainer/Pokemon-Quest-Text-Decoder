using Pokemon_Quest_Text_Decoder.Data;
using Pokemon_Quest_Text_Decoder.Engine;

namespace Pokemon_Quest_Text_Decoder;

public partial class FormMain : Form
{
    private (string path, MessageData data) data;
    private (string path, MessageLabelDataSet data) labelDataSet;
    private string labelFileName = string.Empty;

    private MessageDataEngine engine;

    public bool IsLoaded { get => data.data != null && labelDataSet.data != null; }

    public FormMain()
    {
        InitializeComponent();
        EnableComponents();

        engine = new MessageDataEngine();
    }

    private void EnableComponents()
    {
        rtxtLabel.Enabled = IsLoaded;
        btnSaveLabel.Enabled = IsLoaded;
        numUserParam.Enabled = IsLoaded;
    }

    private void BindListBox()
    {
        var source = labelDataSet.data.Data.Where(d => d.FileName == labelFileName);
        if (source.Any())
            listLabels.DataSource = source.Select(d => d.Id).Take(data.data.LabelCount).ToList();
        else
            listLabels.DataSource = data.data[0];
    }

    private void ReadLabelData(int selectedIndex)
    {
        rtxtLabel.Text = data.data[0, selectedIndex];
        numUserParam.Value = data.data.GetUserParam(0, selectedIndex);
    }

    private void SetLabelData(int selectedIndex)
    {
        data.data[0, selectedIndex] = rtxtLabel.Text;
        data.data.SetUserParam(0, selectedIndex, (ushort)numUserParam.Value);
    }

    private void UpdateListBox()
    {
        if (IsLoaded)
        {
            AutoFindLabelFileName(data.path);
            BindListBox();
            SetLabelData(listLabels.SelectedIndex);
        }
    }

    private void AutoFindLabelFileName(string path)
    {
        string pathFileName = Path.GetFileName(path);
        if (string.IsNullOrEmpty(pathFileName))
        {
            labelFileName = string.Empty;
        }
        else
        {
            int cabIndex = pathFileName.IndexOf("-CAB-");
            if (cabIndex > 0)
                labelFileName = pathFileName.Substring(0, cabIndex);
            else if (labelDataSet.data != null && labelDataSet.data.Data.Any(d => d.FileName == pathFileName))
                labelFileName = pathFileName;
            else
                labelFileName = string.Empty;
        }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            data.data = engine.ReadMessageDataFromBinFile(openFileDialog.FileName);
            data.path = openFileDialog.FileName;
            UpdateListBox();
            EnableComponents();
        }
    }

    private void btnOpenDataSet_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            labelDataSet.data = engine.ReadMessageLabelDataSetFromJSONFile(openFileDialog.FileName);
            labelDataSet.path = openFileDialog.FileName;
            UpdateListBox();
            EnableComponents();
        }
    }

    private void btnSaveTxt_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            engine.SaveMessageDataToTextFile(saveFileDialog.FileName, data.data);
            MessageBox.Show("Successfully exported the text!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnSaveBin_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            engine.SaveMessageDataToBinFile(saveFileDialog.FileName, data.data);
            MessageBox.Show("Successfully exported the re-encoded text!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void listLabels_SelectedIndexChanged(object sender, EventArgs e)
    {
        ReadLabelData(listLabels.SelectedIndex);
    }

    private void btnSaveLabel_Click(object sender, EventArgs e)
    {
        SetLabelData(listLabels.SelectedIndex);
    }
}
