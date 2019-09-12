using System;
using System.Collections.Generic;
using System.Data;
using ERP.DAL;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.Data
{
    public class SubClassRepositroy : ISubClassRepositroy
    {

        private readonly OraDatabase db;
        public SubClassRepositroy(OraDatabase db)
        {
            this.db = db;
        }

        public int Delete(string compCode, string glCode, int id)
        {
            string sql =
                String.Format(@"delete from ACC_SUB_CLASS where AC_CODE||MAIN_CODE||SUB_CODE='{0}' and SUB_CLASS_ID='{1}' and COMP_CODE='{2}'", glCode,id,compCode);
           return db.ExecNoneQuery(sql);
        }



        public List<ACC_SUB_CLASSModel> GetAll(string compId, string acCode, string mainCode)
        {
            string sql = String.Format("select * from ACC_SUB_CLASS where COMP_CODE='{0}' and AC_CODE='{1}' and  MAIN_CODE='{2}' ", compId, acCode, mainCode);
            try
            {
                var obList = new List<ACC_SUB_CLASSModel>();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_SUB_CLASSModel ob = new ACC_SUB_CLASSModel();
                    ob.SUB_CLASS_ID = (dr["SUB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SUB_CLASS_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.SUB_CODE = (dr["SUB_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_CODE"]);
                    ob.SUB_NAME = (dr["SUB_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_NAME"]);
                    ob.MCODE = (dr["MCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCODE"]);
                    ob.SL = (dr["SL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL"]);
                    ob.MAP_CODE = (dr["MAP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAP_CODE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_SUB_CLASSModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public string NextCode(string parenetCode, string comp_code)
        {
            string sql = string.Format("select NVL(MAX(SUB_CODE),'0000') as SUB_CODE  from ACC_SUB_CLASS where COMP_CODE={0} and AC_CODE||MAIN_CODE='{1}'", comp_code, parenetCode);
            DataTable dataTable = db.ExecWithSqlQuery(sql);
            string nextValue = Convert.ToString(dataTable.Rows[0]["SUB_CODE"]);
            return nextValue.PadingWith(4);
        }

        public string Save(ACC_SUB_CLASSModel subclass)
        {

            const string sp = "PKG_ACCOUNTING.acc_sub_class_insert";
            string jsonStr = "{";
            var ob = subclass;
            var i = 1;
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
            {
                 new CommandParameter() {ParameterName = "pSUB_CLASS_ID", Value = ob.SUB_CLASS_ID},
                 new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                 new CommandParameter() {ParameterName = "pAC_CODE", Value = ob.AC_CODE},
                 new CommandParameter() {ParameterName = "pMAIN_CODE", Value = ob.MAIN_CODE},
                 new CommandParameter() {ParameterName = "pSUB_CODE", Value = ob.SUB_CODE},
                 new CommandParameter() {ParameterName = "pSUB_NAME", Value = ob.SUB_NAME},
                 new CommandParameter() {ParameterName = "pMCODE", Value = ob.MCODE},
                 new CommandParameter() {ParameterName = "pSL", Value = ob.SL},
                 new CommandParameter() {ParameterName = "pMAP_CODE", Value = ob.MAP_CODE},
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

        public List<ACC_SUB_CLASSModel> GetAccountHeards(string compCode, string searchKey)
        {


            try
            {
                const string sp = "PKG_ACCOUNTING.acc_gl_account_head_filter";
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                  {

                 new CommandParameter() {ParameterName = "pCOMP_CODE", Value = compCode},
                 new CommandParameter() {ParameterName = "pSEARCH_STRING", Value =searchKey}

             }, sp);
                var obList = new List<ACC_SUB_CLASSModel>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_SUB_CLASSModel ob = new ACC_SUB_CLASSModel();
                    
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.SUB_CODE = (dr["SUB_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_CODE"]);
                    ob.SUB_NAME = (dr["SUB_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_NAME"]);
                   
                    ob.MAIN_NAME = (dr["MAIN_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public List<ACC_SUB_CLASSModel> GetAllByParentCode(string compCode, string pcode)
        {
            string sql = String.Format("select * from ACC_SUB_CLASS where COMP_CODE='{0}' and MAP_CODE='{1}' ", compCode, pcode);
            try
            {
                var obList = new List<ACC_SUB_CLASSModel>();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_SUB_CLASSModel ob = new ACC_SUB_CLASSModel();
                    ob.SUB_CLASS_ID = (dr["SUB_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SUB_CLASS_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.SUB_CODE = (dr["SUB_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_CODE"]);
                    ob.SUB_NAME = (dr["SUB_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_NAME"]);
                    ob.MCODE = (dr["MCODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MCODE"]);
                    ob.SL = (dr["SL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SL"]);
                    ob.MAP_CODE = (dr["MAP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAP_CODE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsSubClassExist(string compCode, string main, long id)
        {
            string sql = String.Format(@"select count(*) from ACC_SUB_CLASS  WHERE UPPER(replace (SUB_NAME,' ',''))= UPPER(replace ('{0}',' ',''))  and COMP_CODE='{1}' and SUB_CLASS_ID<>'{2}' ", main, compCode,id);
            return db.Exist(sql);
        }
    }



}
