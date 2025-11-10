using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace ERP.DAL
{
    public class OraDatabase
    {

        public DataSet ExecuteStoredProcedure(List<CommandParameter> commandParameters, String spName)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new OracleConnection(constr);

            try
            {
                var cm = new OracleCommand(spName, cn)
                {
                    BindByName = true,
                    CommandType = CommandType.StoredProcedure
                };

                cn.Open();
                OracleCommandBuilder.DeriveParameters(cm);
                cn.Close();

                foreach (CommandParameter commandParameter in commandParameters)
                {
                    if (cm.Parameters.Contains(commandParameter.ParameterName))
                    {
                        cm.Parameters[commandParameter.ParameterName].Value = commandParameter.Value;
                    }
                    else
                    {
                        throw new Exception("Store Procedure does not contain parameter : " + commandParameter.ParameterName);
                    }
                }

                var ds = new DataSet();
                var adap = new OracleDataAdapter(cm);
                adap.Fill(ds);

                var dt = new DataTable("OUTPARAM");
                dt.Columns.Add("KEY");
                dt.Columns.Add("VALUE");

                foreach (OracleParameter op in cm.Parameters)
                {
                    if (op.OracleDbType != OracleDbType.RefCursor && (op.Direction == ParameterDirection.InputOutput || op.Direction == ParameterDirection.Output))
                    {
                        DataRow dr = dt.NewRow();
                        dr["KEY"] = op.ParameterName;
                        dr["VALUE"] = op.Value;
                        dt.Rows.Add(dr);
                    }
                }

                ds.Tables.Add(dt);
                cm.Dispose();
                adap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
            }
        }

        public DataSet ExecuteSQLStatement(String SQL)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new OracleConnection(constr);

            try
            {
                var cm = new OracleCommand(SQL, cn);

                cn.Open();
                cm.ExecuteReader();
                cn.Close();

                var ds = new DataSet();
                var adap = new OracleDataAdapter(cm);
                adap.Fill(ds);
                cm.Dispose();
                adap.Dispose();
                return ds;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public DataTable ExecWithSqlQuery(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new OracleConnection(constr);
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand(query, cn);
                DataTable dt = new DataTable();
                OracleDataAdapter dataAdapter = new OracleDataAdapter(cmd);
                dataAdapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if
                    (cn.State == ConnectionState.Open)
                {

                    cn.Close();
                }
            }
        }

        public int ExecNoneQuery(string query)
        {
            

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new OracleConnection(constr);
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand(query, cn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if
                    (cn.State == ConnectionState.Open)
                {

                    cn.Close();
                }
            }
        }
        public bool Exist(string query)
        {
            

            string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            var cn = new OracleConnection(constr);
            try
            {
                cn.Open();
                OracleCommand cmd = new OracleCommand(query, cn);
                object val= cmd.ExecuteScalar();
                int flg = Convert.ToInt32(val);

                return flg>0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if
                    (cn.State == ConnectionState.Open)
                {

                    cn.Close();
                }
            }
        }

    }
    

    //****Helper Class**********************************
    public class CommandParameter
    {
        public String ParameterName { get; set; }
        public Object Value { get; set; }
        public ParameterDirection Direction { get; set; }

        public CommandParameter()
        {
            Direction = ParameterDirection.Input;
        }
    }

}
