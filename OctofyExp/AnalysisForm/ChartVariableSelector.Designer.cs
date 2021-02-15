
namespace OctofyExp
{
    partial class ChartVariableSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartVariableSelector));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.xAxisVariableSelector = new OctofyExp.VariableSelector();
            this.yAxisVariableSelector = new OctofyExp.VariableSelector();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // xAxisVariableSelector
            // 
            this.xAxisVariableSelector.AllowDateColumn = false;
            this.xAxisVariableSelector.DateColumnName = "";
            resources.ApplyResources(this.xAxisVariableSelector, "xAxisVariableSelector");
            this.xAxisVariableSelector.MaxMembers = 64;
            this.xAxisVariableSelector.MeasurementColumn = "X-axis column:";
            this.xAxisVariableSelector.Name = "xAxisVariableSelector";
            this.xAxisVariableSelector.VariableType = OctofyExp.DataAnalysisForm.Views.BarChart;
            this.xAxisVariableSelector.SelectionChanged += new System.EventHandler(this.Variable_SelectedIndexChanged);
            // 
            // yAxisVariableSelector
            // 
            this.yAxisVariableSelector.AllowDateColumn = true;
            this.yAxisVariableSelector.DateColumnName = "";
            resources.ApplyResources(this.yAxisVariableSelector, "yAxisVariableSelector");
            this.yAxisVariableSelector.MaxMembers = 32768;
            this.yAxisVariableSelector.MeasurementColumn = "Y-axis column:";
            this.yAxisVariableSelector.Name = "yAxisVariableSelector";
            this.yAxisVariableSelector.VariableType = OctofyExp.DataAnalysisForm.Views.BarChart;
            this.yAxisVariableSelector.SelectionChanged += new System.EventHandler(this.Variable_SelectedIndexChanged);
            this.yAxisVariableSelector.SelectedDateGroupTypeChanged += new System.EventHandler(this.YAxisVariableSelector_SelectedDateGroupTypeChanged);
            // 
            // ChartVariableSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xAxisVariableSelector);
            this.Controls.Add(this.yAxisVariableSelector);
            this.Name = "ChartVariableSelector";
            this.Resize += new System.EventHandler(this.ChartVariableSelector_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private VariableSelector yAxisVariableSelector;
        private VariableSelector xAxisVariableSelector;
        private System.Windows.Forms.Timer timer;
    }
}
