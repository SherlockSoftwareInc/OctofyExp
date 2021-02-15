using System.Windows.Forms;

namespace DBExpo
{
    public partial class DateRangePickerDialog : Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.byDateTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnSpecifyDate = new System.Windows.Forms.RadioButton();
            this.rbtnLastFiveYears = new System.Windows.Forms.RadioButton();
            this.rbtnLstThreeYears = new System.Windows.Forms.RadioButton();
            this.rbtnLastTwoYears = new System.Windows.Forms.RadioButton();
            this.rbtnLastYear = new System.Windows.Forms.RadioButton();
            this.rbtnThisYear = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.byDateTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.okButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancelButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(122, 296);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(146, 29);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(67, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(76, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(67, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.byDateTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(258, 272);
            this.tabControl1.TabIndex = 0;
            // 
            // byDateTabPage
            // 
            this.byDateTabPage.Controls.Add(this.groupBox1);
            this.byDateTabPage.Controls.Add(this.rbtnSpecifyDate);
            this.byDateTabPage.Controls.Add(this.rbtnLastFiveYears);
            this.byDateTabPage.Controls.Add(this.rbtnLstThreeYears);
            this.byDateTabPage.Controls.Add(this.rbtnLastTwoYears);
            this.byDateTabPage.Controls.Add(this.rbtnLastYear);
            this.byDateTabPage.Controls.Add(this.rbtnThisYear);
            this.byDateTabPage.Location = new System.Drawing.Point(4, 22);
            this.byDateTabPage.Name = "byDateTabPage";
            this.byDateTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.byDateTabPage.Size = new System.Drawing.Size(250, 246);
            this.byDateTabPage.TabIndex = 0;
            this.byDateTabPage.Text = "Date";
            this.byDateTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpEnd);
            this.groupBox1.Controls.Add(this.dtpStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(32, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 79);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(58, 48);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(112, 20);
            this.dtpEnd.TabIndex = 3;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(58, 22);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(112, 20);
            this.dtpStart.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "From:";
            // 
            // rbtnSpecifyDate
            // 
            this.rbtnSpecifyDate.AutoSize = true;
            this.rbtnSpecifyDate.Location = new System.Drawing.Point(16, 128);
            this.rbtnSpecifyDate.Name = "rbtnSpecifyDate";
            this.rbtnSpecifyDate.Size = new System.Drawing.Size(63, 17);
            this.rbtnSpecifyDate.TabIndex = 5;
            this.rbtnSpecifyDate.Text = "Specify:";
            this.rbtnSpecifyDate.UseVisualStyleBackColor = true;
            this.rbtnSpecifyDate.CheckedChanged += new System.EventHandler(this.SpecifyDate_CheckedChanged);
            // 
            // rbtnLastFiveYears
            // 
            this.rbtnLastFiveYears.AutoSize = true;
            this.rbtnLastFiveYears.Location = new System.Drawing.Point(16, 105);
            this.rbtnLastFiveYears.Name = "rbtnLastFiveYears";
            this.rbtnLastFiveYears.Size = new System.Drawing.Size(93, 17);
            this.rbtnLastFiveYears.TabIndex = 4;
            this.rbtnLastFiveYears.Text = "Last five years";
            this.rbtnLastFiveYears.UseVisualStyleBackColor = true;
            // 
            // rbtnLstThreeYears
            // 
            this.rbtnLstThreeYears.AutoSize = true;
            this.rbtnLstThreeYears.Location = new System.Drawing.Point(16, 81);
            this.rbtnLstThreeYears.Name = "rbtnLstThreeYears";
            this.rbtnLstThreeYears.Size = new System.Drawing.Size(100, 17);
            this.rbtnLstThreeYears.TabIndex = 3;
            this.rbtnLstThreeYears.Text = "Last three years";
            this.rbtnLstThreeYears.UseVisualStyleBackColor = true;
            // 
            // rbtnLastTwoYears
            // 
            this.rbtnLastTwoYears.AutoSize = true;
            this.rbtnLastTwoYears.Location = new System.Drawing.Point(16, 58);
            this.rbtnLastTwoYears.Name = "rbtnLastTwoYears";
            this.rbtnLastTwoYears.Size = new System.Drawing.Size(93, 17);
            this.rbtnLastTwoYears.TabIndex = 2;
            this.rbtnLastTwoYears.Text = "Last two years";
            this.rbtnLastTwoYears.UseVisualStyleBackColor = true;
            // 
            // rbtnLastYear
            // 
            this.rbtnLastYear.AutoSize = true;
            this.rbtnLastYear.Location = new System.Drawing.Point(16, 36);
            this.rbtnLastYear.Name = "rbtnLastYear";
            this.rbtnLastYear.Size = new System.Drawing.Size(68, 17);
            this.rbtnLastYear.TabIndex = 1;
            this.rbtnLastYear.Text = "Last year";
            this.rbtnLastYear.UseVisualStyleBackColor = true;
            // 
            // rbtnThisYear
            // 
            this.rbtnThisYear.AutoSize = true;
            this.rbtnThisYear.Checked = true;
            this.rbtnThisYear.Location = new System.Drawing.Point(16, 12);
            this.rbtnThisYear.Name = "rbtnThisYear";
            this.rbtnThisYear.Size = new System.Drawing.Size(68, 17);
            this.rbtnThisYear.TabIndex = 0;
            this.rbtnThisYear.TabStop = true;
            this.rbtnThisYear.Text = "This year";
            this.rbtnThisYear.UseVisualStyleBackColor = true;
            // 
            // DateRangePickerDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(280, 337);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateRangePickerDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Date Range Picker";
            this.Load += new System.EventHandler(this.DateRangePickerDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.byDateTabPage.ResumeLayout(false);
            this.byDateTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Button okButton;
        private Button cancelButton;
        private TabControl tabControl1;
        private TabPage byDateTabPage;
        private GroupBox groupBox1;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
        private Label label2;
        private Label label1;
        private RadioButton rbtnSpecifyDate;
        private RadioButton rbtnLastYear;
        private RadioButton rbtnThisYear;
        private RadioButton rbtnLstThreeYears;
        private RadioButton rbtnLastTwoYears;
        private RadioButton rbtnLastFiveYears;
    }
}