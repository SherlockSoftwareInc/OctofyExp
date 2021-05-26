using OctofyLib;
using System;
using System.Windows.Forms;

namespace OctofyExp
{
    /// <summary>
    /// Chart x-axis and y-axis variable (column) selector
    /// </summary>
    public partial class ChartVariableSelector : UserControl
    {
        public event EventHandler SelectionChanged;

        public event EventHandler SelectedDateGroupTypeChanged;

        public ChartVariableSelector()
        {
            InitializeComponent();
        }

        private string _selectedX = "";
        private string _selectedY = "";
        private VariableSelector _activeControl;

        #region"Properties"

        /// <summary>
        /// Gets or sets date column name
        /// </summary>
        public string DateColumnName { get; set; } = "";


        /// <summary>
        /// Gets or sets maximum category members allows for
        /// x-axis. When members of category exceed the setting,
        /// column will be exclude from selectable list
        /// </summary>
        public int MaxXMembers
        {
            get { return xAxisVariableSelector.MaxMembers; }
            set { xAxisVariableSelector.MaxMembers = value; }
        }

        /// <summary>
        /// Gets or sets maximum category members allows for
        /// y-axis. When members of category exceed the setting,
        /// column will be exclude from selectable list
        /// </summary>
        public int MaxYMembers
        {
            get { return yAxisVariableSelector.MaxMembers; }
            set { yAxisVariableSelector.MaxMembers = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Title
        {
            get
            {
                if (xAxisVariableSelector.Visible)
                {
                    return string.Format(Properties.Resources.A076, YAxisCaption, XAxisCaption);
                }
                else
                {
                    return YAxisCaption;
                }
            }
        }

        /// <summary>
        /// Gets or set chart type so that x-axis variable selection
        /// will be turn off when chart is a 1D chart
        /// </summary>
        public DataAnalysisForm.Views VariableType
        {
            get
            {
                return yAxisVariableSelector.VariableType;
            }

            set
            {
                yAxisVariableSelector.VariableType = value;
                xAxisVariableSelector.VariableType = value;
                Populate();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string XAxisCaption
        {
            get
            {
                return xAxisVariableSelector.Caption;
            }
        }

        /// <summary>
        /// Gets selected variable (column) for y-axis
        /// </summary>
        public string YAxisVariable
        {
            get
            {
                return yAxisVariableSelector.SelectedVariable;
            }
        }

        /// <summary>
        /// Gets selected variable (column) for x-axis
        /// </summary>
        public string XAxisVariable
        {
            get
            {
                return xAxisVariableSelector.SelectedVariable;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string YAxisCaption
        {
            get
            {
                return yAxisVariableSelector.Caption;
            }
        }

        #endregion

        /// <summary>
        /// Open the variable selector control
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="variableType"></param>
        public void Open(TableAnalysis dataSource, DataAnalysisForm.Views variableType)
        {
            yAxisVariableSelector.Open(dataSource, variableType);
            xAxisVariableSelector.Open(dataSource, variableType);
            Populate();
        }

        /// <summary>
        /// Populate the variable selectors
        /// </summary>
        private void Populate()
        {
            xAxisVariableSelector.MeasurementColumn = Properties.Resources.A082;
            if (VariableType == DataAnalysisForm.Views.CategoryBarChart)
            {
                yAxisVariableSelector.MeasurementColumn = Properties.Resources.A047;    //A047: Y-axis column:
                xAxisVariableSelector.Visible = true;
                xAxisVariableSelector.BringToFront();
            }
            else
            {
                xAxisVariableSelector.Visible = false;
                yAxisVariableSelector.MeasurementColumn = Properties.Resources.A048;    //A048: Measurement column:
            }
            _selectedY = YAxisVariable;
            _selectedX = XAxisVariable;

            PerformLayout();

            _activeControl = yAxisVariableSelector;
        }

        /// <summary>
        /// Perform layout for the controls
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (xAxisVariableSelector.Visible)
            {
                int halfHeight = (Height / 2) - 1;
                yAxisVariableSelector.Dock = DockStyle.Top;
                yAxisVariableSelector.Height = halfHeight;
                xAxisVariableSelector.Dock = DockStyle.Bottom;
                xAxisVariableSelector.Height = halfHeight;
            }
            else
            {
                yAxisVariableSelector.Dock = DockStyle.Fill;
            }
        }

        /// <summary>
        /// Handle variable selected index change event:
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Variable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                timer.Stop();
                if (!xAxisVariableSelector.Visible)
                {
                    timer.Interval = 50;
                }
                else
                {
                    timer.Interval = 500;
                }
            }

            if (sender is VariableSelector variableSelector)
            {
                _activeControl = variableSelector;
            }

            timer.Start();
        }

        /// <summary>
        /// Timer tick event handle: delay to fire selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            if (!xAxisVariableSelector.Visible)
            {
                if (YAxisVariable != _selectedY)
                {
                    _selectedY = YAxisVariable;
                    SelectionChanged?.Invoke(this, e);
                }
            }
            else
            {
                if (YAxisVariable != _selectedY || XAxisVariable != _selectedX)
                {
                    _selectedY = YAxisVariable;
                    _selectedX = XAxisVariable;
                    SelectionChanged?.Invoke(this, e);
                }
            }
        }

        /// <summary>
        /// Variable date group selection changed event handle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YAxisVariableSelector_SelectedDateGroupTypeChanged(object sender, EventArgs e)
        {
            if (sender is VariableSelector variableSelector)
            {
                _activeControl = variableSelector;
            }
            SelectedDateGroupTypeChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// Handle control resize event:
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartVariableSelector_Resize(object sender, EventArgs e)
        {
            PerformLayout();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _activeControl?.Focus();
            //base.OnMouseEnter(e);
        }
    }
}