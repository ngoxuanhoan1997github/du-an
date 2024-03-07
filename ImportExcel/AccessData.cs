using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace ImportExcel
{
    public class AccessData
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        private DataSet dts;
        private DataTable tbl;
        private SqlDataReader dataReader;
        private SqlDataAdapter dataAdapter;
        private SqlTransaction transaction;
        private string serverName;
        private string dbName;

        public AccessData(string serverName, string dbName)
        {
            this.serverName = serverName;
            this.dbName = dbName;
        }

        public string chuoiketnoi()
        {
            string chuoikn1 = "Data Source=192.168.24.104;Initial Catalog=QUANLI_XE;User ID=sa;Password=sa";
            return chuoikn1;
        }

        public SqlConnection fBolKetNoi()
        {
            return new SqlConnection(chuoiketnoi());
        }

        public OleDbDataReader docdulieuexcel(string extension, string path, string sheetName)
        {
            OleDbConnection connect = new OleDbConnection() { ConnectionString = getconnectexcel(extension, path) };
            OleDbCommand command = new OleDbCommand(String.Format("select * from [{0}]", sheetName), connect);
            try
            {
                connect.Open();
                OleDbDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                command.Dispose();
                dataReader.Dispose();
            }
        }

        public DataTable Docdulieutext(string pathFile)
        {
            DataTable dt = new DataTable();
            using (TextReader tr = File.OpenText(pathFile))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] items = line.Trim().Split('\t');
                    if (dt.Columns.Count == 0)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            dt.Columns.Add(new DataColumn("Column" + i, typeof(string)));
                        }
                    }
                    dt.Rows.Add(items);
                }
            }
            dt.Rows[0].Delete();
            return dt;
        }

        public string getconnectexcel(string extension, string path)
        {
            string connString = "";
            if (extension.ToLower().Trim() == ".xls")
            {
                connString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", path);
            }
            else
            {
                connString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;", path);
            }
            return connString;
        }

        public DataTable docdulieutableexcel(string extension, string path, string sheetName)
        {
            DataTable dtexcel = new DataTable();
            OleDbConnection connect = new OleDbConnection() { ConnectionString = getconnectexcel(extension, path) };
            OleDbDataAdapter daexcel = new OleDbDataAdapter(String.Format("select * from [{0}]", sheetName), connect);
            try
            {
                connect.Open();
                daexcel.Fill(dtexcel);
                connect.Close();
                return dtexcel;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (connect.State != ConnectionState.Closed) { connect.Close(); }
                connect.Dispose();
                daexcel.Dispose();
            }
        }

        public string[] GetExcelSheetNames(string extension, string path)
        {
            OleDbConnection objConn = null;
            DataTable dt = null;
            try
            {
                objConn = new OleDbConnection(getconnectexcel(extension, path));
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                string[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                return excelSheets;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        public bool fBolThucThiSP(string procedure, SqlParameter[] parameters)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = cnn };
                if (parameters != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
            }
        }

        public bool fBolThucThiSQL(string query)
        {
            try
            {
                cnn = fBolKetNoi();
                transaction = cnn.BeginTransaction();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = query, Connection = cnn, Transaction = transaction };
                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                transaction.Dispose();
            }
        }

        // Thuc thi cau lenh co tham so
        public bool fBolThucThiSPSQL(string procedure, SqlParameter[] parameters)
        {
            try
            {
                cnn = fBolKetNoi();
                transaction = cnn.BeginTransaction();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = cnn, Transaction = transaction };
                if (parameters != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }

                cmd.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                transaction.Dispose();
            }
        }

        //Ðoc dữ liệu vào dataset bằng StoredProcedure có tham số
        public DataSet fdtsDocDuLieuSP(string procedure, SqlParameter[] parameters)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = cnn };
                if (parameters != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }
                dataAdapter = new SqlDataAdapter(cmd);
                dts = new DataSet();
                dataAdapter.Fill(dts);
                cnn.Close();
                return dts;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                dataAdapter.Dispose();
            }
        }

        //Ðoc dữ liệu vào dataset bằng SQL text
        public DataSet fdtsDocDuLieuSQL(string query)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = query, Connection = cnn };
                dataAdapter = new SqlDataAdapter(cmd);
                dts = new DataSet();
                dataAdapter.Fill(dts);
                cnn.Close();
                return dts;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                dataAdapter.Dispose();
            }
        }

        //Đọc dữ liệu vào datatable bằng SQL
        public DataTable ftblDocDuLieuSQL2(string strSQL)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                cmd.Connection = cnn;
                cmd.CommandTimeout = 30000;
                dataAdapter = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                dataAdapter.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                dataAdapter.Dispose();
            }
        }

        //Ðoc dữ liệu vào datatable bằng StoredProcedure
        public DataTable ftblDocDuLieuSP(string procedure, SqlParameter[] parameters)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = cnn };
                if (parameters != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }
                dataAdapter = new SqlDataAdapter(cmd);
                tbl = new DataTable(procedure);
                dataAdapter.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                dataAdapter.Dispose();
            }
        }

        public DataSet fBolMultiSelect(string procedure, SqlParameter[] parameters)
        {
            cnn = fBolKetNoi();
            using (SqlConnection conn = new SqlConnection(cnn.ConnectionString))
            {
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(procedure, conn);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (parameters != null) //Trường hợp có tham số
                {
                    adapter.SelectCommand.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        adapter.SelectCommand.Parameters.Add(parameters[i]);
                    }
                }
                adapter.Fill(dataset);
                return dataset;
            }
        }

        public DataTable ftblDocDuLieuSP_TH(string procedure, SqlParameter[] parameters)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = cnn };
                if (parameters != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }
                dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.SelectCommand.CommandTimeout = 120;
                tbl = new DataTable(procedure);
                dataAdapter.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                dataAdapter.Dispose();
            }
        }

        //Ðoc dữ liệu vào datatable bằng SQL text
        public DataTable ftblDocDuLieuSQL(string query)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = query, Connection = cnn };
                dataAdapter = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                dataAdapter.Fill(tbl);
                cnn.Close();
                return tbl;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (cnn.State != ConnectionState.Closed) { cnn.Close(); }
                cnn.Dispose();
                cmd.Dispose();
                dataAdapter.Dispose();
            }
        }

        //Ðoc dữ liệu vào datareader bằng StoredProcedure
        public SqlDataReader fdreadDocDuLieuSP(string procedure, SqlParameter[] parameters)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = cnn };
                if (parameters != null) //Trường hợp có tham số
                {
                    cmd.Parameters.Clear();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        cmd.Parameters.Add(parameters[i]);
                    }
                }
                dataReader = cmd.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //Ðoc dữ liệu vào datareader bằng SQL text
        public SqlDataReader fdreadDocDuLieuSQL(string query)
        {
            try
            {
                cnn = fBolKetNoi();
                cmd = new SqlCommand() { CommandType = CommandType.Text, CommandText = query, Connection = cnn };
                dataReader = cmd.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Hàm dùng để import dữ liệu từ Excel vào Db
        /// Có sử dụng rollback nếu import thất bại
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool ImportExcel(string procedure, SqlParameter[,] parameters)
        {
            using (SqlConnection connection = new SqlConnection(chuoiketnoi()))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    cmd = new SqlCommand() { CommandType = CommandType.StoredProcedure, CommandText = procedure, Connection = connection, Transaction = transaction };
                    if (parameters != null)
                    {
                        long x = parameters.GetLength(0);
                        long y = parameters.GetLength(1);
                        for (int i = 0; i < x; i++)
                        {
                            cmd.Parameters.Clear();
                            for (int j = 0; j < y; j++)
                            {
                                cmd.Parameters.Add(parameters[i, j]);
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }
    }
}