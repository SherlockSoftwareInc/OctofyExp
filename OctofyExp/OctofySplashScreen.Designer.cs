using System.Windows.Forms;

namespace OctofyExp
{
    partial class OctofySplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OctofySplashScreen));
            this.applicationTitleLabel = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.octofyLogoControl = new OctofyLib.OctofyRing();
            this.editionLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.websiteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // applicationTitleLabel
            // 
            resources.ApplyResources(this.applicationTitleLabel, "applicationTitleLabel");
            this.applicationTitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.applicationTitleLabel.Name = "applicationTitleLabel";
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.copyrightLabel, "copyrightLabel");
            this.copyrightLabel.Name = "copyrightLabel";
            // 
            // versionLabel
            // 
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.versionLabel, "versionLabel");
            this.versionLabel.Name = "versionLabel";
            // 
            // subtitleLabel
            // 
            resources.ApplyResources(this.subtitleLabel, "subtitleLabel");
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Name = "subtitleLabel";
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            // 
            // octofyLogoControl
            // 
            this.octofyLogoControl.Animation = false;
            this.octofyLogoControl.Colors = "#94C600,#94C600,#94C600,#94C600,#94C600,#71685A,#FF6700,#909465,#956B43,#FEA022";
            this.octofyLogoControl.InitialAngle = 0F;
            resources.ApplyResources(this.octofyLogoControl, "octofyLogoControl");
            this.octofyLogoControl.Name = "octofyLogoControl";
            this.octofyLogoControl.SaveFolderName = "";
            this.octofyLogoControl.Values = new int[] {
        48,
        9,
        21,
        18,
        16,
        49};
            // 
            // editionLabel
            // 
            resources.ApplyResources(this.editionLabel, "editionLabel");
            this.editionLabel.BackColor = System.Drawing.Color.Transparent;
            this.editionLabel.ForeColor = System.Drawing.Color.DodgerBlue;
            this.editionLabel.Name = "editionLabel";
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.CloseButtonTimer_Tick);
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
            this.descriptionLabel.Name = "descriptionLabel";
            // 
            // websiteButton
            // 
            resources.ApplyResources(this.websiteButton, "websiteButton");
            this.websiteButton.Name = "websiteButton";
            this.websiteButton.UseVisualStyleBackColor = true;
            this.websiteButton.Click += new System.EventHandler(this.WebsiteButton_Click);
            // 
            // OctofySplashScreen
            // 
            this.AcceptButton = this.closeButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ControlBox = false;
            this.Controls.Add(this.websiteButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.octofyLogoControl);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.editionLabel);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.applicationTitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OctofySplashScreen";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.OctofySplashScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private Label applicationTitleLabel;
        private Label versionLabel;
        private Label copyrightLabel;
        private OctofyLib.OctofyRing octofyLogoControl;
        private Label subtitleLabel;
        private Timer timer1;
        private Label editionLabel;
        private Button closeButton;
        private Timer timer2;
        private Label descriptionLabel;
        private Button websiteButton;
    }
}
