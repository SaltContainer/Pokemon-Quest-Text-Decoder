﻿namespace Pokemon_Quest_Text_Decoder;

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
        btnOpen = new Button();
        btnSaveTxt = new Button();
        btnSaveLabel = new Button();
        btnSaveBin = new Button();
        listLabels = new ListBox();
        rtxtLabel = new RichTextBox();
        numUserParam = new NumericUpDown();
        grpLabel = new GroupBox();
        lbUserParam = new Label();
        btnOpenDataSet = new Button();
        ((System.ComponentModel.ISupportInitialize)numUserParam).BeginInit();
        grpLabel.SuspendLayout();
        SuspendLayout();
        // 
        // btnOpen
        // 
        btnOpen.Image = Resources.Resources.folder;
        btnOpen.Location = new Point(12, 12);
        btnOpen.Name = "btnOpen";
        btnOpen.Size = new Size(122, 45);
        btnOpen.TabIndex = 0;
        btnOpen.Text = "Open Bin";
        btnOpen.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnOpen.UseVisualStyleBackColor = true;
        btnOpen.Click += btnOpen_Click;
        // 
        // btnSaveTxt
        // 
        btnSaveTxt.Image = Resources.Resources.save;
        btnSaveTxt.Location = new Point(268, 12);
        btnSaveTxt.Name = "btnSaveTxt";
        btnSaveTxt.Size = new Size(118, 45);
        btnSaveTxt.TabIndex = 1;
        btnSaveTxt.Text = "Save Text";
        btnSaveTxt.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSaveTxt.UseVisualStyleBackColor = true;
        btnSaveTxt.Click += btnSaveTxt_Click;
        // 
        // btnSaveLabel
        // 
        btnSaveLabel.Location = new Point(126, 170);
        btnSaveLabel.Name = "btnSaveLabel";
        btnSaveLabel.Size = new Size(110, 38);
        btnSaveLabel.TabIndex = 2;
        btnSaveLabel.Text = "Save Label";
        btnSaveLabel.UseVisualStyleBackColor = true;
        btnSaveLabel.Click += btnSaveLabel_Click;
        // 
        // btnSaveBin
        // 
        btnSaveBin.Image = Resources.Resources.save;
        btnSaveBin.Location = new Point(392, 12);
        btnSaveBin.Name = "btnSaveBin";
        btnSaveBin.Size = new Size(118, 45);
        btnSaveBin.TabIndex = 3;
        btnSaveBin.Text = "Save Bin";
        btnSaveBin.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSaveBin.UseVisualStyleBackColor = true;
        btnSaveBin.Click += btnSaveBin_Click;
        // 
        // listLabels
        // 
        listLabels.FormattingEnabled = true;
        listLabels.ItemHeight = 15;
        listLabels.Location = new Point(12, 63);
        listLabels.Name = "listLabels";
        listLabels.Size = new Size(250, 214);
        listLabels.TabIndex = 4;
        listLabels.SelectedIndexChanged += listLabels_SelectedIndexChanged;
        // 
        // rtxtLabel
        // 
        rtxtLabel.Location = new Point(6, 22);
        rtxtLabel.Name = "rtxtLabel";
        rtxtLabel.Size = new Size(230, 90);
        rtxtLabel.TabIndex = 5;
        rtxtLabel.Text = "";
        // 
        // numUserParam
        // 
        numUserParam.Location = new Point(82, 118);
        numUserParam.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
        numUserParam.Name = "numUserParam";
        numUserParam.Size = new Size(154, 23);
        numUserParam.TabIndex = 6;
        // 
        // grpLabel
        // 
        grpLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        grpLabel.Controls.Add(lbUserParam);
        grpLabel.Controls.Add(numUserParam);
        grpLabel.Controls.Add(rtxtLabel);
        grpLabel.Controls.Add(btnSaveLabel);
        grpLabel.Location = new Point(268, 63);
        grpLabel.Name = "grpLabel";
        grpLabel.Size = new Size(242, 214);
        grpLabel.TabIndex = 7;
        grpLabel.TabStop = false;
        grpLabel.Text = "Label Data";
        // 
        // lbUserParam
        // 
        lbUserParam.AutoSize = true;
        lbUserParam.Location = new Point(6, 120);
        lbUserParam.Name = "lbUserParam";
        lbUserParam.Size = new Size(70, 15);
        lbUserParam.TabIndex = 7;
        lbUserParam.Text = "User Param:";
        // 
        // btnOpenDataSet
        // 
        btnOpenDataSet.Image = Resources.Resources.folder;
        btnOpenDataSet.Location = new Point(140, 12);
        btnOpenDataSet.Name = "btnOpenDataSet";
        btnOpenDataSet.Size = new Size(122, 45);
        btnOpenDataSet.TabIndex = 8;
        btnOpenDataSet.Text = "Open Label DataSet";
        btnOpenDataSet.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnOpenDataSet.UseVisualStyleBackColor = true;
        btnOpenDataSet.Click += btnOpenDataSet_Click;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(522, 294);
        Controls.Add(grpLabel);
        Controls.Add(listLabels);
        Controls.Add(btnSaveBin);
        Controls.Add(btnSaveTxt);
        Controls.Add(btnOpenDataSet);
        Controls.Add(btnOpen);
        Name = "FormMain";
        Text = "Pokémon Quest Text Editor";
        ((System.ComponentModel.ISupportInitialize)numUserParam).EndInit();
        grpLabel.ResumeLayout(false);
        grpLabel.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Button btnOpen;
    private Button btnSaveTxt;
    private Button btnSaveLabel;
    private Button btnSaveBin;
    private ListBox listLabels;
    private RichTextBox rtxtLabel;
    private NumericUpDown numUserParam;
    private GroupBox grpLabel;
    private Label lbUserParam;
    private Button btnOpenDataSet;
}
