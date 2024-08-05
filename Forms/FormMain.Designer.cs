namespace Pokemon_Quest_Text_Decoder;

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
        btnTest = new Button();
        btnSaveBin = new Button();
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
        btnSaveTxt.Location = new Point(140, 12);
        btnSaveTxt.Name = "btnSaveTxt";
        btnSaveTxt.Size = new Size(118, 45);
        btnSaveTxt.TabIndex = 1;
        btnSaveTxt.Text = "Save Text";
        btnSaveTxt.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSaveTxt.UseVisualStyleBackColor = true;
        btnSaveTxt.Click += btnSaveTxt_Click;
        // 
        // btnTest
        // 
        btnTest.Location = new Point(310, 182);
        btnTest.Name = "btnTest";
        btnTest.Size = new Size(110, 38);
        btnTest.TabIndex = 2;
        btnTest.Text = "Test";
        btnTest.UseVisualStyleBackColor = true;
        btnTest.Click += btnTest_Click;
        // 
        // btnSaveBin
        // 
        btnSaveBin.Image = Resources.Resources.save;
        btnSaveBin.Location = new Point(264, 12);
        btnSaveBin.Name = "btnSaveBin";
        btnSaveBin.Size = new Size(118, 45);
        btnSaveBin.TabIndex = 3;
        btnSaveBin.Text = "Save Bin";
        btnSaveBin.TextImageRelation = TextImageRelation.ImageBeforeText;
        btnSaveBin.UseVisualStyleBackColor = true;
        btnSaveBin.Click += btnSaveBin_Click;
        // 
        // FormMain
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(483, 294);
        Controls.Add(btnTest);
        Controls.Add(btnSaveBin);
        Controls.Add(btnSaveTxt);
        Controls.Add(btnOpen);
        Name = "FormMain";
        Text = "Pokémon Quest Text Editor";
        ResumeLayout(false);
    }

    #endregion

    private Button btnOpen;
    private Button btnSaveTxt;
    private Button btnTest;
    private Button btnSaveBin;
}
