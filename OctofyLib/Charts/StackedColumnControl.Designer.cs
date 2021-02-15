using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class StackedColumnControl : UserControl
    {

        // UserControl overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // StackedBarControl
            // 
            AutoScaleDimensions = new SizeF(8.0F, 16.0F);
            AutoScaleMode = AutoScaleMode.Font;
            DoubleBuffered = true;
            Name = "StackedBarControl";
            Size = new Size(837, 608);
            Load += new EventHandler(StackedBarControl_Load);
            Paint += new PaintEventHandler(StackedBarControl_Paint);
            ClientSizeChanged += new EventHandler(StackedBarControl_ClientSizeChanged);
            MouseMove += new MouseEventHandler(StackedBarControl_MouseMove);
            MouseClick += new MouseEventHandler(StackedBarControl_MouseClick);
            ResumeLayout(false);
        }

        private ToolTip toolTip1;
    }
}