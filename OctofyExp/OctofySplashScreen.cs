using System;
using System.Reflection;
using System.Windows.Forms;

namespace OctofyExp
{
    public partial class OctofySplashScreen : Form
    {
        public OctofySplashScreen()
        {
            InitializeComponent();
            this.copyrightLabel.Text = AssemblyCopyright;
            this.versionLabel.Text = String.Format(Properties.Resources.A083, AssemblyVersion);
            //editionLabel.Text = "Express";
            //subtitleLabel.Text = "SQL Server Visualized";
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// Gets assembly version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Get product name from assemble
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// Get copyright info from assemble
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }
        #endregion

        private void OctofySplashScreen_Load(object sender, EventArgs e)
        {
            octofyLogoControl.Colors = "#94C600,#94C600,#94C600,#94C600,#94C600,#71685A,#FF6700,#909465,#956B43,#FEA022";
            octofyLogoControl.Animation = true;
            timer2.Start();

        }

        private void CloseButtonTimer_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            closeButton.Visible = true;
            websiteButton.Visible = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WebsiteButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://sherlocksoftwareinc.godaddysites.com/");
        }
    }
}
