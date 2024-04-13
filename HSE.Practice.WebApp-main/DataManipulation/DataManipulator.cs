using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;

namespace DataManipulation
{
    public class DataManipulator : IExcelExport, IExcelImport
    {
        #region Fields

        private DataTable inp_data;
        private DataTable first_data;
        private Application objExcel;
        private Workbook workbook;

        #endregion

        #region IExcelExport functions

        public void ExportFile
        (
            string filePath,
            string roomName,
            DataTable dt
        )
        {
            generateExcel(roomName, dt);
            File.Delete(filePath);
            workbook.SaveAs
                (
                 filePath
                );
            workbook.Close();
            objExcel.Quit();
        }

        private void generateExcel(string room_name, DataTable dt)
        {
            objExcel = new Application();
            workbook = objExcel.Workbooks.Add();
            Worksheet sheet = workbook.ActiveSheet;
            //Set main info
            var range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 3]];
            range.Cells[1, 1] = "v. 1";
            range.Cells[1, 2] = "Дата: " + DateTime.Now.ToShortDateString();
            range.Cells[1, 3] = "Помещение: " + room_name;

            //Set columns info
            range = sheet.Range[sheet.Cells[2, 1], sheet.Cells[2, 4]];
            range.Cells[1, 1] = "Наименование";
            range.Cells[1, 2] = "Семейство";
            range.Cells[1, 3] = "Состояние";
            range.Cells[1, 4] = "Значение";
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            range.Cells.Interior.Color = Color.Yellow;


            //Fill 
            range = sheet.Range[sheet.Cells[3, 1], sheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]];
            for (var i = 0; i < dt.Rows.Count; ++i)
                for (var j = 0; j < dt.Columns.Count; ++j)
                    range.Cells[i + 1, j + 1] = dt.Rows[i][j].ToString();
            range.Cells.Borders.LineStyle = XlLineStyle.xlContinuous;
            sheet.Cells.EntireColumn.AutoFit();
            sheet.Cells.EntireRow.AutoFit();
            sheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
            sheet.PageSetup.Zoom = false;
            sheet.PageSetup.FitToPagesWide = 1;
            sheet.PageSetup.FitToPagesTall = false;
            sheet.PageSetup.ScaleWithDocHeaderFooter = true;
            sheet.PageSetup.AlignMarginsHeaderFooter = true;
        }

        #endregion

        #region IExcelImport functions

        /// <summary>
        ///     is the same data
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool is_contains(string name)
        {
            for (var i = 0; i < inp_data.Rows.Count; ++i)
                if (inp_data.Rows[i].ItemArray[0].ToString() == name)
                    return true;

            return false;
        }

        private void Parser(string path)
        {
            var fInfo = new FileInfo(path);
            try
            {
                if (fInfo.Extension != ".xlsx")
                    return;

                objExcel = new Application();
                workbook = objExcel.Workbooks.Open(fInfo.FullName);
                var worksheet = workbook.Worksheets[1] as Worksheet;
                var range = worksheet.UsedRange;

                inp_data = new DataTable();
                var values = (object[,])range.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                var arr = new string[values.GetLength(0), values.GetLength(1)];

                for (var row = 1; row <= worksheet.UsedRange.Columns.Count; ++row)
                {
                    if (values[1, row] as string == "" || values[1, row] as string == null)
                    {
                        inp_data = null;
                        return;
                    }

                    switch (row)
                    {
                        case 1:
                            {
                                if (values[1, row].ToString() != "Наименование")
                                {
                                    inp_data = null;
                                    return;
                                }

                                inp_data.Columns.Add("Наименование");
                                break;
                            }
                        case 2:
                            {
                                if (values[1, row].ToString() != "Семейство")
                                {
                                    inp_data = null;
                                    return;
                                }

                                inp_data.Columns.Add("Семейство");
                                break;
                            }
                        case 3:
                            {
                                if (values[1, row].ToString() != "Состояние")
                                {
                                    inp_data = null;
                                    return;
                                }

                                inp_data.Columns.Add("Состояние");
                                break;
                            }
                        case 4:
                            {
                                if (values[1, row].ToString() != "Значение")
                                {
                                    inp_data = null;
                                    return;
                                }

                                inp_data.Columns.Add("Значение");
                                break;
                            }
                        default:
                            inp_data = null;
                            return;
                    }
                }

                if (worksheet.UsedRange.Columns.Count != 4 ||
                    worksheet.UsedRange.Rows.Count - 1 != first_data.Rows.Count)
                {
                    inp_data = null;
                    return;
                }

                for (var row = 2; row <= worksheet.UsedRange.Rows.Count; ++row)
                {
                    var arr_row = new string[4];
                    for (var col = 1; col <= worksheet.UsedRange.Columns.Count; ++col)
                    {
                        if (values[row, col] as string == "" || values[row, col] as string == null)
                        {
                            inp_data = null;
                            return;
                        }

                        //access each cell
                        switch (col)
                        {
                            case 1:
                                {
                                    if (is_contains(values[row, col].ToString()))
                                    {
                                        inp_data = null;
                                        return;
                                    }

                                    var ok = true;
                                    for (var i = 0; i < first_data.Rows.Count; ++i)
                                        if (first_data.Rows[i].ItemArray[0].ToString() == values[row, col].ToString())
                                        {
                                            arr_row[0] = values[row, col].ToString();
                                            ok = false;
                                            break;
                                        }

                                    if (!ok) break;

                                    inp_data = null;
                                    return;
                                }
                            case 2:
                                {
                                    var ok = true;
                                    for (var i = 0; i < first_data.Rows.Count; ++i)
                                        if (first_data.Rows[i].ItemArray[1].ToString() == values[row, col].ToString())
                                        {
                                            arr_row[1] = values[row, col].ToString();
                                            ok = false;
                                            break;
                                        }

                                    if (!ok) break;

                                    inp_data = null;
                                    return;
                                }
                            case 3:
                                {
                                    if (values[row, col].ToString() != "Включен" &&
                                        values[row, col].ToString() != "Выключен")
                                    {
                                        inp_data = null;
                                        return;
                                    }

                                    arr_row[2] = values[row, col].ToString();
                                    break;
                                }
                            case 4:
                                {
                                    if (values[row, col].ToString() != "Открыто" &&
                                        values[row, col].ToString() != "Закрыто")
                                    {
                                        inp_data = null;
                                        return;
                                    }

                                    arr_row[3] = values[row, col].ToString();
                                    break;
                                }
                        }
                    }

                    inp_data.Rows.Add(arr_row);
                }
            }
            catch { }
        }

        /// <summary>
        ///     Input excel file
        /// </summary>
        /// <returns></returns>
        public DataTable InputFile(string path, DataTable dt)
        {
            first_data = dt;
            inp_data = new DataTable();
            Parser(path);
            try
            {
                workbook.Close();
                objExcel.Quit();
            }
            catch { }
            File.Delete(path);

            return inp_data;
        }

        #endregion
    }
}