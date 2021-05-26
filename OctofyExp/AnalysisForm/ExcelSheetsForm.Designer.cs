
namespace OctofyExp
{
    partial class ExcelSheetsForm
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
            this.components = new System.ComponentModel.Container();
            this.selectSheetLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.analysisButton = new System.Windows.Forms.Button();
            this.sheetsListBox = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.infoToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.startTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectSheetLabel
            // 
            this.selectSheetLabel.AutoSize = true;
            this.selectSheetLabel.Location = new System.Drawing.Point(12, 9);
            this.selectSheetLabel.Name = "selectSheetLabel";
            this.selectSheetLabel.Size = new System.Drawing.Size(163, 13);
            this.selectSheetLabel.TabIndex = 6;
            this.selectSheetLabel.Text = "Please select a sheet to analysis:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(237, 63);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "&Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // analysisButton
            // 
            this.analysisButton.Enabled = false;
            this.analysisButton.Location = new System.Drawing.Point(237, 25);
            this.analysisButton.Name = "analysisButton";
            this.analysisButton.Size = new System.Drawing.Size(75, 23);
            this.analysisButton.TabIndex = 5;
            this.analysisButton.Text = "&Analysis";
            this.analysisButton.UseVisualStyleBackColor = true;
            this.analysisButton.Click += new System.EventHandler(this.AnalysisButton_Click);
            // 
            // sheetsListBox
            // 
            this.sheetsListBox.FormattingEnabled = true;
            this.sheetsListBox.Location = new System.Drawing.Point(12, 25);
            this.sheetsListBox.Name = "sheetsListBox";
            this.sheetsListBox.Size = new System.Drawing.Size(219, 381);
            this.sheetsListBox.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 414);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(322, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // infoToolStripStatusLabel
            // 
            this.infoToolStripStatusLabel.Name = "infoToolStripStatusLabel";
            this.infoToolStripStatusLabel.Size = new System.Drawing.Size(307, 17);
            this.infoToolStripStatusLabel.Spring = true;
            this.infoToolStripStatusLabel.Text = "Reading data...";
            this.infoToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // startTimer
            // 
            this.startTimer.Interval = 10;
            this.startTimer.Tick += new System.EventHandler(this.StartTimer_Tick);
            // 
            // ExcelSheetsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 436);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.selectSheetLabel);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.analysisButton);
            this.Controls.Add(this.sheetsListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExcelSheetsForm";
            this.Text = "Excel Analysis";
            this.Load += new System.EventHandler(this.ExcelSheetsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label selectSheetLabel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button analysisButton;
        private System.Windows.Forms.ListBox sheetsListBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel infoToolStripStatusLabel;
        private System.Windows.Forms.Timer startTimer;
    }
}