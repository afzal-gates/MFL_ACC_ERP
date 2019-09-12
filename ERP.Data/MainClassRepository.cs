using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.Data
{
    public class MainClassRepository : IMainClassRepository
    {

        private readonly OraDatabase db;
        public MainClassRepository(OraDatabase db)
        {
            this.db = db;
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }



        public ACC_MAIN_CLASSModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public string NextCode(string accode, string compId)
        {
           string sql = string.Format("select NVL(MAX(MAIN_CODE),'00') as MAIN_CODE  from ACC_MAIN_CLASS where COMP_CODE='{0}' and AC_CODE='{1}'",compId,accode);
           DataTable dataTable = db.ExecWithSqlQuery(sql);
           string nextValue= Convert.ToString(dataTable.Rows[0]["MAIN_CODE"]);
           return  nextValue.PadingWith(2);
        }

        public List<SelectModel> GetCashBankHeads(string compCode, string mapCode)
        {
            string sql = String.Format("select AC_CODE||MAIN_CODE||SUB_CODE AS CODE,SUB_NAME from ACC_SUB_CLASS where COMP_CODE='{0}' and MAP_CODE in({1}) ", compCode, mapCode);
            try
            {
                var obList = new List<SelectModel>();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SelectModel ob = new SelectModel();
                    ob.Value = (dr["CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CODE"]);
                    ob.Text = (dr["SUB_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Save(ACC_MAIN_CLASSModel vtm)
        {
            const string sp = "PKG_ACCOUNTING.acc_main_class_insert";
            string jsonStr = "{";
            var ob = vtm;
            var i = 1;
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMAIN_CLASS_ID", Value = ob.MAIN_CLASS_ID},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "pAC_CODE", Value = ob.AC_CODE},
                     new CommandParameter() {ParameterName = "pMAIN_CODE", Value = ob.MAIN_CODE},
                     new CommandParameter() {ParameterName = "pMAIN_NAME", Value = ob.MAIN_NAME},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    

        List<ACC_MAIN_CLASSModel> IMainClassRepository.GetAll(string compId,string acCode)
        {
            string sql = String.Format("select * from ACC_MAIN_CLASS where COMP_CODE='{0}' and AC_CODE='{1}' ", compId, acCode);
            try
            {
                var obList = new List<ACC_MAIN_CLASSModel>();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_MAIN_CLASSModel ob = new ACC_MAIN_CLASSModel();
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.MAIN_NAME = (dr["MAIN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_NAME"]);
                    ob.MAIN_CLASS_ID = (dr["MAIN_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAIN_CLASS_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public bool IsMainClassExist(string compCode,  string mainName,long id)
        {
            string sql = String.Format(@"select count(*) from ACC_MAIN_CLASS  WHERE UPPER(replace (MAIN_NAME,' ',''))= UPPER(replace ('{0}',' ',''))  and COMP_CODE='{1}' and MAIN_CLASS_ID<> '{2}' ", mainName, compCode, id);
            return db.Exist(sql);
        }
    }
}
