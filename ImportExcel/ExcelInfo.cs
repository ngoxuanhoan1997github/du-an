using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ImportExcel
{
    public partial class ExcelInfo : DevExpress.XtraEditors.XtraForm
    {
        public ExcelInfo()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int firstRow, lastRow;
            string sheet = txtSheet.Text;
            if (int.TryParse(txtFirstRow.Text, out firstRow) && int.TryParse(txtLastRow.Text, out lastRow))
            {
                if (firstRow > 0 && lastRow > 0)
                {
                    if (firstRow <= lastRow)
                    {
                        if (onGetSizeClose != null)
                        {
                            FileSize fileSize = new FileSize(firstRow, lastRow, sheet);
                            onGetSizeClose(EventArgs.Empty, fileSize);
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Dòng cuối phải lớn hơn dòng đầu !!");
                    }
                }
                else
                {
                    MessageBox.Show("Dòng đầu và dòng cuối phải lớn hơn 0 !!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng !!");
            }
        }
        private event EventHandler<FileSize> onGetSizeClose;

        public event EventHandler<FileSize> OnGetSizeClose
        {
            add { onGetSizeClose += value; }
            remove { onGetSizeClose -= value; }
        }
    }
    public class FileSize : EventArgs
    {
        public int FirstRow { get; set; }
        public int LastRow { get; set; }
        public string Sheet { get; set; }

        public FileSize(int firstRow, int lastRow, string sheet)
        {
            this.FirstRow = firstRow;
            this.LastRow = lastRow;
            this.Sheet = sheet;
        }
    }
}