using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    public partial class LegendPanel : UserControl
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
            // LegendPanel
            // 
            AutoScaleDimensions = new SizeF(6.0F, 13.0F);
            AutoScaleMode = AutoScaleMode.Font;
            Margin = new Padding(2, 2, 2, 2);
            Name = "LegendPanel";
            Size = new Size(451, 122);
            ClientSizeChanged += new EventHandler(LegendPanel_ClientSizeChanged);
            Paint += new PaintEventHandler(LegendPanel_Paint);
            MouseMove += new MouseEventHandler(LegendPanel_MouseMove);
            ResumeLayout(false);
        }

        private ToolTip toolTip1;

    }
}