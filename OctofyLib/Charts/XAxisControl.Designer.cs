using System;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyLib
{
    partial class XAxisControl
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
            SuspendLayout();
            components = new System.ComponentModel.Container();
            this.AutoScaleDimensions = new SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            Name = "UserControl1";
            Size = new Size(827, 52);
            Paint += new PaintEventHandler(XAxisControl_Paint);
            Resize += new EventHandler(XAxisControl_Resize);
            ResumeLayout(false);

        }

        #endregion
    }
}
