using QuestTextEditor.Data;
using QuestTextEditor.Engine;

namespace QuestTextEditor;

public partial class FormMain : Form
{
    private (string path, MessageData data) data;
    private (string path, MessageLabelDataSet data) labelDataSet;

    private MessageDataEngine engine;

    public bool IsLoaded { get => data.data != null && labelDataSet.data != null; }
    public int CurrentLanguage { get => (int)numLang.Value; }
    public int CurrentLabel { get => listLabels.SelectedIndex; }

    public FormMain()
    {
        InitializeComponent();
        EnableComponents();

        engine = new MessageDataEngine();
    }

    private void EnableComponents()
    {
        btnOpen.Enabled = true;
        btnOpenDataSet.Enabled = true;

        btnImportCSV.Enabled = IsLoaded;
        btnSaveCSV.Enabled = IsLoaded;
        btnSaveBin.Enabled = IsLoaded;

        rtxtLabel.Enabled = IsLoaded;
        btnSaveLabel.Enabled = IsLoaded;
        numUserParam.Enabled = IsLoaded;

        numLang.Enabled = IsLoaded;
    }

    private void BindListBox(int lang)
    {
        if (data.data.LabelNames.Any())
            listLabels.DataSource = data.data.LabelNames.Take(data.data.LabelCount).ToList();
        else
            listLabels.DataSource = data.data.GetDefaultLabelRepresentations(lang);
    }

    private void ReadLabelData(int lang, int selectedIndex)
    {
        rtxtLabel.Text = data.data[lang, selectedIndex];
        numUserParam.Value = data.data.GetUserParam(lang, selectedIndex);
    }

    private void SetLabelData(int lang, int selectedIndex)
    {
        data.data[lang, selectedIndex] = rtxtLabel.Text;
        data.data.SetUserParam(lang, selectedIndex, (ushort)numUserParam.Value);
    }

    private void UpdateListBox()
    {
        if (IsLoaded)
        {
            data.data.FileName = AutoFindLabelFileName(data.path);
            data.data.SetUpLabelNames(labelDataSet.data);
            UpdateLanguage(CurrentLanguage);
            SetLabelData(CurrentLanguage, CurrentLabel);
        }
    }

    private void UpdateLangBox()
    {
        if (IsLoaded)
        {
            numLang.Maximum = data.data.LanguageCount - 1;
        }
    }

    private string AutoFindLabelFileName(string path)
    {
        string pathFileName = Path.GetFileName(path);
        if (string.IsNullOrEmpty(pathFileName))
        {
            return string.Empty;
        }
        else
        {
            int cabIndex = pathFileName.IndexOf("-CAB-");
            if (cabIndex > 0)
                return pathFileName.Substring(0, cabIndex);
            else if (labelDataSet.data?[pathFileName].Any() ?? false)
                return pathFileName;
            else
                return string.Empty;
        }
    }

    private void UpdateLanguage(int lang)
    {
        if (IsLoaded)
        {
            BindListBox(lang);
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
            UpdateLangBox();
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
            UpdateLangBox();
            EnableComponents();
        }
    }

    private void btnImportCSV_Click(object sender, EventArgs e)
    {
        using OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            engine.ReadCSVFileIntoMessageData(openFileDialog.FileName, data.data, CurrentLanguage);
            UpdateListBox();
            UpdateLangBox();
        }
    }

    private void btnSaveTxt_Click(object sender, EventArgs e)
    {
        using SaveFileDialog saveFileDialog = new SaveFileDialog();
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            engine.SaveMessageDataToCSVFile(saveFileDialog.FileName, data.data, CurrentLanguage);
            MessageBox.Show("Successfully exported the CSV!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        ReadLabelData(CurrentLanguage, CurrentLabel);
    }

    private void btnSaveLabel_Click(object sender, EventArgs e)
    {
        SetLabelData(CurrentLanguage, CurrentLabel);
    }

    private void numLang_ValueChanged(object sender, EventArgs e)
    {
        UpdateLanguage(CurrentLanguage);
    }
}
