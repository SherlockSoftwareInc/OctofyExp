namespace OctofyLib
{
    partial class VScrollBarChartControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.scrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // scrollBar
            // 
            this.scrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.scrollBar.Location = new System.Drawing.Point(574, 0);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(17, 408);
            this.scrollBar.TabIndex = 2;
            this.scrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            this.scrollBar.GotFocus += new System.EventHandler(this.ScrollBar_GotFocus);

            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scrollBar);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(200, 20);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(591, 408);
            this.Load += new System.EventHandler(this.VScrollBarChartControl_Load);
            this.ClientSizeChanged += new System.EventHandler(this.VScrollBarChartControl_ClientSizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VScrollBarChartControl_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VScrollBarChartControl_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VScrollBarChartControl_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VScrollBarChartControl_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.VScrollBarChartControl_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.VScrollBar scrollBar;
    }
}
