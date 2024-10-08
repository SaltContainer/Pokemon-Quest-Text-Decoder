﻿namespace QuestTextEditor;

partial class FormMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
        btnOpen = new Button();
        btnSaveCSV = new Button();
        btnSaveLabel = new Button();
        btnSaveBin = new Button();
        listLabels = new ListBox();
        rtxtLabel = new RichTextBox();
        numUserParam = new NumericUpDown();
        grpLabel = new GroupBox();
        lbUserParam = new Label();
        btnOpenDataSet = new Button();
        numLang = new NumericUpDown();
        lbLang = new Label();
        btnImportCSV = new Button();
        checkEncoded = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)numUserParam).BeginInit();
        grpLabel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numLang).BeginInit();
        SuspendLayout();
        // 
        // btnOpen
        // 
        btnOpen.Image = Resources.Resources.folder;
        btnOpen.Location = new Point(12, 12);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(120, 45);
        btnOpen.TabIndex = 0;
        btnOpen.Text = "Open Bin";
        btnOpen.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnOpen.UseVisualStyleBackColor = true;
        btnOpen.Click += btnOpen_Click;
        // 
        // btnSaveCSV
        // 
        btnSaveCSV.Image = Resources.Resources.save;
        btnSaveCSV.Location = new Point(264, 12);
        btnSaveCSV.Name = "btnSaveCSV";
        btnSaveCSV.Size = new Size(120, 45);
        btnSaveCSV.TabIndex = 2;
        btnSaveCSV.Text = "Save as CSV";
        btnSaveCSV.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSaveCSV.UseVisualStyleBackColor = true;
        btnSaveCSV.Click += btnSaveTxt_Click;
        // 
        // btnSaveLabel
        // 
        btnSaveLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSaveLabel.Location = new Point(130, 170);
        btnSaveLabel.Name = "btnSaveLabel";
        btnSaveLabel.Size = new Size(110, 38);
        btnSaveLabel.TabIndex = 7;
        btnSaveLabel.Text = "Save Label";
        btnSaveLabel.UseVisualStyleBackColor = true;
        btnSaveLabel.Click += btnSaveLabel_Click;
        // 
        // btnSaveBin
        // 
        btnSaveBin.Image = Resources.Resources.save;
        btnSaveBin.Location = new Point(390, 12);
        btnSaveBin.Name = "btnSaveBin";
        btnSaveBin.Size = new Size(120, 45);
        btnSaveBin.TabIndex = 3;
        btnSaveBin.Text = "Save Bin";
        btnSaveBin.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSaveBin.UseVisualStyleBackColor = true;
        btnSaveBin.Click += btnSaveBin_Click;
        // 
        // listLabels
        // 
        listLabels.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        listLabels.FormattingEnabled = true;
        listLabels.ItemHeight = 15;
        listLabels.Location = new Point(12, 143);
        listLabels.Name = "listLabels";
        listLabels.Size = new Size(246, 214);
        listLabels.TabIndex = 4;
        listLabels.SelectedIndexChanged += listLabels_SelectedIndexChanged;
        // 
        // rtxtLabel
        // 
        rtxtLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        rtxtLabel.Location = new Point(6, 22);
        rtxtLabel.Name = "rtxtLabel";
        rtxtLabel.Size = new Size(234, 113);
        rtxtLabel.TabIndex = 5;
        rtxtLabel.Text = "";
        // 
        // numUserParam
        // 
        numUserParam.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        numUserParam.Location = new Point(82, 141);
        numUserParam.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        numUserParam.Name = "numUserParam";
        numUserParam.Size = new Size(158, 23);
        numUserParam.TabIndex = 6;
        // 
        // grpLabel
        // 
        grpLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        grpLabel.Controls.Add(lbUserParam);
        grpLabel.Controls.Add(numUserParam);
        grpLabel.Controls.Add(rtxtLabel);
        grpLabel.Controls.Add(btnSaveLabel);
        grpLabel.Location = new Point(264, 143);
        grpLabel.Name = "grpLabel";
        grpLabel.Size = new Size(246, 214);
        grpLabel.TabIndex = 7;
        grpLabel.TabStop = false;
        grpLabel.Text = "Label Data";
        // 
        // lbUserParam
        // 
        lbUserParam.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lbUserParam.AutoSize = true;
        lbUserParam.Location = new Point(6, 143);
        lbUserParam.Name = "lbUserParam";
        lbUserParam.Size = new Size(70, 15);
        lbUserParam.TabIndex = 7;
        lbUserParam.Text = "User Param:";
        // 
        // btnOpenDataSet
        // 
        btnOpenDataSet.Image = Resources.Resources.folder;
        btnOpenDataSet.Location = new Point(138, 12);
        btnOpenDataSet.Name = "btnOpenDataSet";
        btnOpenDataSet.Size = new Size(120, 45);
        btnOpenDataSet.TabIndex = 1;
        btnOpenDataSet.Text = "Open Label DataSet";
        btnOpenDataSet.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnOpenDataSet.UseVisualStyleBackColor = true;
        btnOpenDataSet.Click += btnOpenDataSet_Click;
        // 
        // numLang
        // 
        numLang.Location = new Point(80, 76);
        numLang.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
        numLang.Name = "numLang";
        numLang.Size = new Size(52, 23);
        numLang.TabIndex = 8;
        numLang.ValueChanged += numLang_ValueChanged;
        // 
        // lbLang
        // 
        lbLang.AutoSize = true;
        lbLang.Location = new Point(12, 78);
        lbLang.Name = "lbLang";
        lbLang.Size = new Size(62, 15);
        lbLang.TabIndex = 9;
        lbLang.Text = "Language:";
        // 
        // btnImportCSV
        // 
        btnImportCSV.Image = Resources.Resources.folder;
        btnImportCSV.Location = new Point(138, 63);
        btnImportCSV.Name = "btnImportCSV";
        btnImportCSV.Size = new Size(120, 45);
        btnImportCSV.TabIndex = 10;
        btnImportCSV.Text = "Import CSV";
        btnImportCSV.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnImportCSV.UseVisualStyleBackColor = true;
        btnImportCSV.Click += btnImportCSV_Click;
        // 
        // checkEncoded
        // 
        checkEncoded.AutoSize = true;
        checkEncoded.Checked = true;
        checkEncoded.CheckState = CheckState.Checked;
        checkEncoded.Location = new Point(264, 63);
        checkEncoded.Name = "checkEncoded";
        checkEncoded.Size = new Size(170, 19);
        checkEncoded.TabIndex = 11;
        checkEncoded.Text = "Coded Mode for Bin Export";
        checkEncoded.UseVisualStyleBackColor = true;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(522, 369);
        Controls.Add(checkEncoded);
        Controls.Add(lbLang);
        Controls.Add(numLang);
        Controls.Add(grpLabel);
        Controls.Add(listLabels);
        Controls.Add(btnSaveBin);
        Controls.Add(btnSaveCSV);
        Controls.Add(btnImportCSV);
        Controls.Add(btnOpenDataSet);
        Controls.Add(btnOpen);
        Icon = (Icon)resources.GetObject("$this.Icon");
        MinimumSize = new Size(538, 363);
        Name = "FormMain";
        Text = "Quest Text Editor";
        ((System.ComponentModel.ISupportInitialize)numUserParam).EndInit();
        grpLabel.ResumeLayout(false);
        grpLabel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numLang).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnOpen;
    private Button btnSaveCSV;
    private Button btnSaveLabel;
    private Button btnSaveBin;
    private ListBox listLabels;
    private RichTextBox rtxtLabel;
    private NumericUpDown numUserParam;
    private GroupBox grpLabel;
    private Label lbUserParam;
    private Button btnOpenDataSet;
    private NumericUpDown numLang;
    private Label lbLang;
    private Button btnImportCSV;
    private CheckBox checkEncoded;
}
