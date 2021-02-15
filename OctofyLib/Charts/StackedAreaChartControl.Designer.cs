namespace OctofyLib
{
    partial class StackedAreaChartControl
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
            this.SuspendLayout();
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(567, 453);
            this.Load += new System.EventHandler(this.StackedBarControl_Load);
            this.ClientSizeChanged += new System.EventHandler(this.StackedBarControl_ClientSizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StackedBarControl_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StackedAreaChartControl_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StackedBarControl_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
    }
}
