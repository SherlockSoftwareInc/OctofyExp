
namespace OctofyExp
{
    partial class DataLoaderForm
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
            this.loadingLabel = new System.Windows.Forms.Label();
            this.loadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new System.Windows.Forms.Button();
            this.progressTimer = new System.Windows.Forms.Timer(this.components);
            this.startTimer = new System.Windows.Forms.Timer(this.components);
            this.numRowsLabel = new System.Windows.Forms.Label();
            this.exitTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Location = new System.Drawing.Point(24, 73);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(158, 13);
            this.loadingLabel.TabIndex = 0;
            this.loadingLabel.Text = "Please wait while loading data...";
            // 
            // loadingProgressBar
            // 
            this.loadingProgressBar.Location = new System.Drawing.Point(24, 44);
            this.loadingProgressBar.Name = "loadingProgressBar";
            this.loadingProgressBar.Size = new System.Drawing.Size(294, 23);
            this.loadingProgressBar.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(137, 103);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // progressTimer
            // 
            this.progressTimer.Interval = 500;
            this.progressTimer.Tick += new System.EventHandler(this.ProgressTimer_Tick);
            // 
            // startTimer
            // 
            this.startTimer.Interval = 2;
            this.startTimer.Tick += new System.EventHandler(this.StartTimer_Tick);
            // 
            // numRowsLabel
            // 
            this.numRowsLabel.AutoSize = true;
            this.numRowsLabel.Location = new System.Drawing.Point(24, 24);
            this.numRowsLabel.Name = "numRowsLabel";
            this.numRowsLabel.Size = new System.Drawing.Size(158, 13);
            this.numRowsLabel.TabIndex = 0;
            this.numRowsLabel.Text = "There are 1,000 rows to read in.";
            // 
            // exitTimer
            // 
            this.exitTimer.Interval = 5;
            this.exitTimer.Tick += new System.EventHandler(this.ExitTimer_Tick);
            // 
            // DataLoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 147);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loadingProgressBar);
            this.Controls.Add(this.numRowsLabel);
            this.Controls.Add(this.loadingLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DataLoaderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataLoaderForm";
            this.Load += new System.EventHandler(this.DataLoaderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.ProgressBar loadingProgressBar;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Timer progressTimer;
        private System.Windows.Forms.Timer startTimer;
        private System.Windows.Forms.Label numRowsLabel;
        private System.Windows.Forms.Timer exitTimer;
    }
}