
namespace ExcelTester
{
    partial class SheetPickerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sheetsListBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.selectSheetLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sheetsListBox
            // 
            this.sheetsListBox.FormattingEnabled = true;
            this.sheetsListBox.Location = new System.Drawing.Point(12, 25);
            this.sheetsListBox.Name = "sheetsListBox";
            this.sheetsListBox.Size = new System.Drawing.Size(219, 381);
            this.sheetsListBox.TabIndex = 0;
            this.sheetsListBox.SelectedIndexChanged += new System.EventHandler(this.sheetsListBox_SelectedIndexChanged);
            // 
            // okButton
            // 
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(237, 25);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "&Analysis";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(237, 63);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "&Close";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // selectSheetLabel
            // 
            this.selectSheetLabel.AutoSize = true;
            this.selectSheetLabel.Location = new System.Drawing.Point(12, 9);
            this.selectSheetLabel.Name = "selectSheetLabel";
            this.selectSheetLabel.Size = new System.Drawing.Size(111, 13);
            this.selectSheetLabel.TabIndex = 2;
            this.selectSheetLabel.Text = "Please select a sheet:";
            // 
            // SheetPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 425);
            this.Controls.Add(this.selectSheetLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.sheetsListBox);
            this.Name = "SheetPickerForm";
            this.Text = "SheetPickerForm";
            this.Load += new System.EventHandler(this.SheetPickerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox sheetsListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label selectSheetLabel;
    }
}