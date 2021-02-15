using System;
using System.Drawing;
using System.Windows.Forms;

namespace OctofyExp
{
    internal static class Module1
    {
        internal static bool IsOnScreen(Form form)
        {
            Screen[] screens = Screen.AllScreens;
            foreach (Screen screen in screens)
            {
                Point formTopLeft = new Point(form.Left, form.Top);

                if (screen.WorkingArea.Contains(formTopLeft))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Export a DataGridView to an Excel work sheet
        /// </summary>
        /// <param name="dgv">DataGridView to output</param>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="autofilter"></param>
        internal static void DataGridViewToExcel(DataGridView dgv, int startRow = 1, int startCol = 1, bool autofilter = true)
        {
            try
            {
                object misValue = System.Reflection.Missing.Value;

                //Start Excel and get Application object.
                var xlApp = new Microsoft.Office.Interop.Excel.Application()
                {
                    Visible = false,
                    UserControl = false
                };

                Microsoft.Office.Interop.Excel._Workbook xlWorkbook = xlApp.Workbooks.Add("");
                Microsoft.Office.Interop.Excel._Worksheet xlSheet = xlWorkbook.ActiveSheet;

                // count visible columns
                int colCount = 0;
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        colCount++;
                    }
                }

                var dgArray = new object[dgv.RowCount, colCount + 1];
                int j = 0;
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    int col = 0;
                    if (r.IsNewRow) continue;
                    //foreach (DataGridViewCell j in r.Cells)
                    //{
                    //    dgArray[j.RowIndex, j.ColumnIndex] = j.Value.ToString();
                    //}
                    for (int i = 0; i < colCount; i++)
                    {
                        if (dgv.Columns[i].Visible)
                        {
                            dgArray[j, col] = r.Cells[col].Value.ToString();
                            col++;
                        }
                    }
                    j++;
                }

                Microsoft.Office.Interop.Excel.Range chartRange;
                int rowCount = dgArray.GetLength(0);
                int columnCount = dgArray.GetLength(1);
                chartRange = (Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[startRow + 1, startCol];
                chartRange = chartRange.get_Resize(rowCount, columnCount);
                chartRange.set_Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault, dgArray);

                // output table header
                int hcol = startCol;
                for (int i = 0; i < colCount; i++)
                {
                    if (dgv.Columns[i].Visible)
                    {
                        xlSheet.Cells[startRow, hcol] = dgv.Columns[i].HeaderText;
                        hcol++;
                    }
                }
                //for column header
                var columnsNameRange = xlSheet.Range[xlSheet.Cells[startRow, startCol],
                    xlSheet.Cells[startRow, dgv.Columns.Count]];
                columnsNameRange.Font.Bold = true;
                columnsNameRange.Interior.ColorIndex = 15;
                columnsNameRange.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;
                if (autofilter)
                    columnsNameRange.AutoFilter();

                // autofit output columns
                var xlCellrange = xlSheet.Range[xlSheet.Cells[startRow + 1, startCol],
                    xlSheet.Cells[dgv.Rows.Count + 1, dgv.Columns.Count]];
                xlCellrange.EntireColumn.AutoFit();

                // show excel and allow user control
                xlApp.Visible = true;
                xlApp.UserControl = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.A008, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //internal static DialogResult ShowInputDialog(ref string input)
        //{
        //    System.Drawing.Size size = new System.Drawing.Size(200, 70);
        //    Form inputBox = new Form()
        //    {
        //        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog,
        //        ClientSize = size,
        //        Text = "Name"
        //    };

        //    var textBox = new TextBox()
        //    {
        //        Size = new System.Drawing.Size(size.Width - 10, 23),
        //        Location = new System.Drawing.Point(5, 5),
        //        Text = input
        //    };

        //    Button okButton = new Button()
        //    {
        //        DialogResult = System.Windows.Forms.DialogResult.OK,
        //        Name = "okButton",
        //        Size = new System.Drawing.Size(75, 23),
        //        Text = "&OK",
        //        Location = new System.Drawing.Point(size.Width - 80 - 80, 39)
        //    };

        //    Button cancelButton = new Button()
        //    {
        //        DialogResult = System.Windows.Forms.DialogResult.Cancel,
        //        Name = "cancelButton",
        //        Size = new System.Drawing.Size(75, 23),
        //        Text = "&Cancel",
        //        Location = new System.Drawing.Point(size.Width - 80, 39)
        //    };

        //    inputBox.Controls.Add(textBox);
        //    inputBox.Controls.Add(okButton);
        //    inputBox.Controls.Add(cancelButton);

        //    inputBox.AcceptButton = okButton;
        //    inputBox.CancelButton = cancelButton;

        //    DialogResult result = inputBox.ShowDialog();
        //    input = textBox.Text;
        //    return result;
        //}

    }
}