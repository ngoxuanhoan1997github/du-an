using ImportExcel.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImportExcel
{
    public partial class Import : DevExpress.XtraEditors.XtraForm
    {
        private int firstRow;
        private int lastRow;
        private string sheet;
        clsImport clsEX = new clsImport();
        public Import()
        {
            InitializeComponent();
        }

        private void getSize_OnGetSizeClose(object sender, FileSize e)
        {
            this.firstRow = e.FirstRow;
            this.lastRow = e.LastRow;
            this.sheet = e.Sheet;
        }

        private ImportData[] GetDataFromExcelFile(string fileName, int firstRow, int lastRow, string sheet)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            object _sheet;
            if (sheet == "")
            {
                _sheet = 1;
            }
            else
            {
                _sheet = sheet;
            }

            int rCnt;
            int len = lastRow - firstRow;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(_sheet);

            range = xlWorkSheet.UsedRange;

            ImportData[] data = new ImportData[len + 1];
            int index = 0;
            for (rCnt = firstRow; rCnt <= lastRow; rCnt++)
            {
                ImportData importdata = new ImportData();
                importdata.soxe = ((range.Cells[rCnt, 1] as Excel.Range).Value2);
                importdata.hangxe = ((range.Cells[rCnt, 2] as Excel.Range).Value2);
                importdata.nguoilai = ((range.Cells[rCnt, 3] as Excel.Range).Value2);
                importdata.sdt = ((range.Cells[rCnt, 4] as Excel.Range).Value2);
                data[index] = importdata;
                index++;
            }
            return data;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                ExcelInfo getSize = new ExcelInfo();
                getSize.OnGetSizeClose += getSize_OnGetSizeClose;
                getSize.ShowDialog();

                if (this.firstRow > 0 && this.lastRow > 0)
                {
                    ImportData[] data = GetDataFromExcelFile(fileName, firstRow, lastRow, sheet);

                    //Tạo mảng 2 chiều
                    //Với x là số dòng trong file Excel
                    //Với y là số field cần thêm cho mỗi dòng
                    int x = data.Length;
                    int y = 4;
                    int j = 0;
                    SqlParameter[,] parameters = new SqlParameter[x, y];
                    for (int i = 0; i < x; i++)
                    {
                        parameters[i, 0] = new SqlParameter("@soxe", SqlDbType.VarChar);
                        parameters[i, 0].Value = data[j].soxe;
                        parameters[i, 1] = new SqlParameter("@hangxe", SqlDbType.VarChar);
                        parameters[i, 1].Value = data[j].hangxe;
                        parameters[i, 2] = new SqlParameter("@nguoilai", SqlDbType.VarChar);
                        parameters[i, 2].Value = data[j].nguoilai;
                        parameters[i, 3] = new SqlParameter("@sdt", SqlDbType.VarChar);
                        parameters[i, 3].Value = data[j].sdt;
                        j++;
                    }
                    bool result = clsEX.ImportExcelTable(parameters);
                    if (result)
                    {
                        MessageBox.Show("Import thành công !!");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi trong lúc xử lý, import thất bại !!");
                    }
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }
    }
}