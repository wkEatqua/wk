namespace JsonConverter
{
    partial class JsonConverter
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
            this.ExcelLoadTextBox = new System.Windows.Forms.TextBox();
            this.ExcelLoadButton = new System.Windows.Forms.Button();
            this.ExcelOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExcelLabel = new System.Windows.Forms.Label();
            this.ExportButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // excelLoadTextBox
            // 
            this.ExcelLoadTextBox.Location = new System.Drawing.Point(9, 27);
            this.ExcelLoadTextBox.MaximumSize = new System.Drawing.Size(261, 92);
            this.ExcelLoadTextBox.MinimumSize = new System.Drawing.Size(261, 92);
            this.ExcelLoadTextBox.Multiline = true;
            this.ExcelLoadTextBox.Name = "excelLoadTextBox";
            this.ExcelLoadTextBox.ReadOnly = true;
            this.ExcelLoadTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ExcelLoadTextBox.Size = new System.Drawing.Size(261, 92);
            this.ExcelLoadTextBox.TabIndex = 0;
            this.ExcelLoadTextBox.TextChanged += new System.EventHandler(this.ExcelLoadTextBox_TextChanged);
            // 
            // excelLoadButton
            // 
            this.ExcelLoadButton.Location = new System.Drawing.Point(276, 27);
            this.ExcelLoadButton.Name = "excelLoadButton";
            this.ExcelLoadButton.Size = new System.Drawing.Size(75, 23);
            this.ExcelLoadButton.TabIndex = 1;
            this.ExcelLoadButton.Text = "Load";
            this.ExcelLoadButton.UseVisualStyleBackColor = true;
            this.ExcelLoadButton.Click += new System.EventHandler(this.ExcelLoadButton_Click);
            // 
            // excelOpenFileDialog
            // 
            this.ExcelOpenFileDialog.FileName = "openFileDialog1";
            // 
            // excelLabel
            // 
            this.ExcelLabel.AutoSize = true;
            this.ExcelLabel.Location = new System.Drawing.Point(9, 9);
            this.ExcelLabel.Name = "excelLabel";
            this.ExcelLabel.Size = new System.Drawing.Size(63, 15);
            this.ExcelLabel.TabIndex = 2;
            this.ExcelLabel.Text = "Excel Data";
            // 
            // ExportButton
            // 
            this.ExportButton.Location = new System.Drawing.Point(276, 96);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(75, 23);
            this.ExportButton.TabIndex = 3;
            this.ExportButton.Text = "Export";
            this.ExportButton.UseVisualStyleBackColor = true;
            this.ExportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // ResultBox
            // 
            this.ResultTextBox.Enabled = false;
            this.ResultTextBox.Location = new System.Drawing.Point(9, 125);
            this.ResultTextBox.Name = "ResultBox";
            this.ResultTextBox.Size = new System.Drawing.Size(342, 23);
            this.ResultTextBox.TabIndex = 4;
            // 
            // JsonConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 156);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.ExcelLabel);
            this.Controls.Add(this.ExcelLoadButton);
            this.Controls.Add(this.ExcelLoadTextBox);
            this.Name = "JsonConverter";
            this.Text = "JsonConverter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label ExcelLabel;
        private TextBox ExcelLoadTextBox;
        private Button ExcelLoadButton;
        
        private OpenFileDialog ExcelOpenFileDialog;

        private Button ExportButton;
        private TextBox ResultTextBox;

        private List<string> excelFilePaths = new List<string>();
    }
}