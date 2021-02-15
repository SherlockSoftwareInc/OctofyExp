namespace OctofyExp
{
    partial class Logo
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
            this.octofyRing1 = new OctofyLib.OctofyRing();
            this.SuspendLayout();
            // 
            // octofyRing1
            // 
            this.octofyRing1.Animation = false;
            this.octofyRing1.BackColor = System.Drawing.Color.Transparent;
            this.octofyRing1.Colors = "#94C600,#94C600,#71685A,#FF6700,#909465,#956B43,#FEA022";
            this.octofyRing1.InitialAngle = 0F;
            this.octofyRing1.Location = new System.Drawing.Point(35, 32);
            this.octofyRing1.MinimumSize = new System.Drawing.Size(16, 16);
            this.octofyRing1.Name = "octofyRing1";
            this.octofyRing1.SaveFolderName = "";
            this.octofyRing1.Size = new System.Drawing.Size(400, 400);
            this.octofyRing1.TabIndex = 0;
            this.octofyRing1.Values = new int[] {
        48,
        9,
        21,
        18,
        16,
        49};
            // 
            // Logo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 597);
            this.Controls.Add(this.octofyRing1);
            this.Name = "Logo";
            this.Text = "Logo";
            this.Load += new System.EventHandler(this.Logo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OctofyLib.OctofyRing octofyRing1;
    }
}