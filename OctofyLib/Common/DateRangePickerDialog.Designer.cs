using System.Windows.Forms;

namespace OctofyLib
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
            this.specifyDateGroupBox = new System.Windows.Forms.GroupBox();
            this.endDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.specifyDateRadioButton = new System.Windows.Forms.RadioButton();
            this.lastFiveYearsRadioButton = new System.Windows.Forms.RadioButton();
            this.lastThreeYearsRadioButton = new System.Windows.Forms.RadioButton();
            this.lastTwoYearsRadioButton = new System.Windows.Forms.RadioButton();
            this.lastYearRadioButton = new System.Windows.Forms.RadioButton();
            this.thisYearRadioButton = new System.Windows.Forms.RadioButton();
            this.lastTenYearRadioButton = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.byDateTabPage.SuspendLayout();
            this.specifyDateGroupBox.SuspendLayout();
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(119, 320);
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
            this.tabControl1.Size = new System.Drawing.Size(256, 295);
            this.tabControl1.TabIndex = 0;
            // 
            // byDateTabPage
            // 
            this.byDateTabPage.Controls.Add(this.specifyDateGroupBox);
            this.byDateTabPage.Controls.Add(this.specifyDateRadioButton);
            this.byDateTabPage.Controls.Add(this.lastTenYearRadioButton);
            this.byDateTabPage.Controls.Add(this.lastFiveYearsRadioButton);
            this.byDateTabPage.Controls.Add(this.lastThreeYearsRadioButton);
            this.byDateTabPage.Controls.Add(this.lastTwoYearsRadioButton);
            this.byDateTabPage.Controls.Add(this.lastYearRadioButton);
            this.byDateTabPage.Controls.Add(this.thisYearRadioButton);
            this.byDateTabPage.Location = new System.Drawing.Point(4, 22);
            this.byDateTabPage.Name = "byDateTabPage";
            this.byDateTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.byDateTabPage.Size = new System.Drawing.Size(248, 269);
            this.byDateTabPage.TabIndex = 0;
            this.byDateTabPage.Text = "Date";
            this.byDateTabPage.UseVisualStyleBackColor = true;
            // 
            // specifyDateGroupBox
            // 
            this.specifyDateGroupBox.Controls.Add(this.endDateDateTimePicker);
            this.specifyDateGroupBox.Controls.Add(this.startDateDateTimePicker);
            this.specifyDateGroupBox.Controls.Add(this.label2);
            this.specifyDateGroupBox.Controls.Add(this.label1);
            this.specifyDateGroupBox.Enabled = false;
            this.specifyDateGroupBox.Location = new System.Drawing.Point(29, 174);
            this.specifyDateGroupBox.Name = "specifyDateGroupBox";
            this.specifyDateGroupBox.Size = new System.Drawing.Size(187, 79);
            this.specifyDateGroupBox.TabIndex = 7;
            this.specifyDateGroupBox.TabStop = false;
            this.specifyDateGroupBox.Text = "Custom";
            // 
            // endDateDateTimePicker
            // 
            this.endDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endDateDateTimePicker.Location = new System.Drawing.Point(58, 48);
            this.endDateDateTimePicker.Name = "endDateDateTimePicker";
            this.endDateDateTimePicker.Size = new System.Drawing.Size(112, 20);
            this.endDateDateTimePicker.TabIndex = 3;
            // 
            // startDateDateTimePicker
            // 
            this.startDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startDateDateTimePicker.Location = new System.Drawing.Point(58, 22);
            this.startDateDateTimePicker.Name = "startDateDateTimePicker";
            this.startDateDateTimePicker.Size = new System.Drawing.Size(112, 20);
            this.startDateDateTimePicker.TabIndex = 1;
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
            // specifyDateRadioButton
            // 
            this.specifyDateRadioButton.AutoSize = true;
            this.specifyDateRadioButton.Location = new System.Drawing.Point(13, 151);
            this.specifyDateRadioButton.Name = "specifyDateRadioButton";
            this.specifyDateRadioButton.Size = new System.Drawing.Size(63, 17);
            this.specifyDateRadioButton.TabIndex = 6;
            this.specifyDateRadioButton.Text = "Specify:";
            this.specifyDateRadioButton.UseVisualStyleBackColor = true;
            this.specifyDateRadioButton.CheckedChanged += new System.EventHandler(this.SpecifyDate_CheckedChanged);
            // 
            // lastFiveYearsRadioButton
            // 
            this.lastFiveYearsRadioButton.AutoSize = true;
            this.lastFiveYearsRadioButton.Location = new System.Drawing.Point(13, 105);
            this.lastFiveYearsRadioButton.Name = "lastFiveYearsRadioButton";
            this.lastFiveYearsRadioButton.Size = new System.Drawing.Size(93, 17);
            this.lastFiveYearsRadioButton.TabIndex = 4;
            this.lastFiveYearsRadioButton.Text = "Last five years";
            this.lastFiveYearsRadioButton.UseVisualStyleBackColor = true;
            // 
            // lastThreeYearsRadioButton
            // 
            this.lastThreeYearsRadioButton.AutoSize = true;
            this.lastThreeYearsRadioButton.Location = new System.Drawing.Point(13, 81);
            this.lastThreeYearsRadioButton.Name = "lastThreeYearsRadioButton";
            this.lastThreeYearsRadioButton.Size = new System.Drawing.Size(100, 17);
            this.lastThreeYearsRadioButton.TabIndex = 3;
            this.lastThreeYearsRadioButton.Text = "Last three years";
            this.lastThreeYearsRadioButton.UseVisualStyleBackColor = true;
            // 
            // lastTwoYearsRadioButton
            // 
            this.lastTwoYearsRadioButton.AutoSize = true;
            this.lastTwoYearsRadioButton.Location = new System.Drawing.Point(13, 58);
            this.lastTwoYearsRadioButton.Name = "lastTwoYearsRadioButton";
            this.lastTwoYearsRadioButton.Size = new System.Drawing.Size(93, 17);
            this.lastTwoYearsRadioButton.TabIndex = 2;
            this.lastTwoYearsRadioButton.Text = "Last two years";
            this.lastTwoYearsRadioButton.UseVisualStyleBackColor = true;
            // 
            // lastYearRadioButton
            // 
            this.lastYearRadioButton.AutoSize = true;
            this.lastYearRadioButton.Location = new System.Drawing.Point(13, 36);
            this.lastYearRadioButton.Name = "lastYearRadioButton";
            this.lastYearRadioButton.Size = new System.Drawing.Size(68, 17);
            this.lastYearRadioButton.TabIndex = 1;
            this.lastYearRadioButton.Text = "Last year";
            this.lastYearRadioButton.UseVisualStyleBackColor = true;
            // 
            // thisYearRadioButton
            // 
            this.thisYearRadioButton.AutoSize = true;
            this.thisYearRadioButton.Checked = true;
            this.thisYearRadioButton.Location = new System.Drawing.Point(13, 12);
            this.thisYearRadioButton.Name = "thisYearRadioButton";
            this.thisYearRadioButton.Size = new System.Drawing.Size(68, 17);
            this.thisYearRadioButton.TabIndex = 0;
            this.thisYearRadioButton.TabStop = true;
            this.thisYearRadioButton.Text = "This year";
            this.thisYearRadioButton.UseVisualStyleBackColor = true;
            // 
            // lastTenYearRadioButton
            // 
            this.lastTenYearRadioButton.AutoSize = true;
            this.lastTenYearRadioButton.Location = new System.Drawing.Point(13, 128);
            this.lastTenYearRadioButton.Name = "lastTenYearRadioButton";
            this.lastTenYearRadioButton.Size = new System.Drawing.Size(91, 17);
            this.lastTenYearRadioButton.TabIndex = 5;
            this.lastTenYearRadioButton.Text = "Last ten years";
            this.lastTenYearRadioButton.UseVisualStyleBackColor = true;
            // 
            // DateRangePickerDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(277, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DateRangePickerDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Date Range Dialog";
            this.Load += new System.EventHandler(this.DateRangePickerDialog_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.byDateTabPage.ResumeLayout(false);
            this.byDateTabPage.PerformLayout();
            this.specifyDateGroupBox.ResumeLayout(false);
            this.specifyDateGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Button okButton;
        private Button cancelButton;
        private TabControl tabControl1;
        private TabPage byDateTabPage;
        private GroupBox specifyDateGroupBox;
        private DateTimePicker endDateDateTimePicker;
        private DateTimePicker startDateDateTimePicker;
        private Label label2;
        private Label label1;
        private RadioButton specifyDateRadioButton;
        private RadioButton lastYearRadioButton;
        private RadioButton thisYearRadioButton;
        private RadioButton lastThreeYearsRadioButton;
        private RadioButton lastTwoYearsRadioButton;
        private RadioButton lastFiveYearsRadioButton;
        private RadioButton lastTenYearRadioButton;
    }
}