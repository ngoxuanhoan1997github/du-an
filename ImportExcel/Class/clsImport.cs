using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace ImportExcel.Class
{
    public class clsImport
    {
        private AccessData ac;

        public clsImport()
        {
            ConnectionString connectStr = new ConnectionString();
            ac = new AccessData(connectStr.ServerName, connectStr.DbName);
        }
        public bool ImportExcelTable(SqlParameter[,] parameters)
        {
            return ac.ImportExcel("ImportExcel_TableImport", parameters);
        }
    }
    internal class ConnectionString
    {
        public string ServerName { get; set; }
        public string DbName { get; set; }

        public ConnectionString()
        {
            this.ServerName = "192.168.24.104";
            this.DbName = "QUANLI_XE";
        }
    }
}
