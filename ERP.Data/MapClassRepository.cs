using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.Data
{
    public class MapClassRepository : IMapClassRepository
    {
          private readonly OraDatabase _db;
          public MapClassRepository(OraDatabase db)
        {
            this._db = db;
        }

          public List<ACC_MAP_CLASS> GetAll(string compCode, string accode, string mainCode,string parentCode)
        {

            try
            {
                var obList = new List<ACC_MAP_CLASS>();
                string sql = String.Format("select * from ACC_MAP_CLASS where COMP_CODE='{0}' and AC_CODE='{1}' and MAIN_CODE='{2}' and PRNT_CODE='{3}' order by MAP_CODE", compCode, accode, mainCode, parentCode);
                var ds = _db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_MAP_CLASS ob = new ACC_MAP_CLASS();
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.MAP_NAME = (dr["MAP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAP_NAME"]);
                    ob.MAP_CODE = (dr["MAP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAP_CODE"]);
                    ob.PRNT_CODE = (dr["PRNT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRNT_CODE"]);
                    ob.MAP_CLASS_ID = (dr["MAP_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAP_CLASS_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


          public string NextCode(string compCode)
          {
              string sql = string.Format("select NVL(MAX(MAP_CODE),'0000') as MAP_CODE  from ACC_MAP_CLASS where COMP_CODE={0} ", compCode);
              DataTable dataTable = _db.ExecWithSqlQuery(sql);
              string nextValue = Convert.ToString(dataTable.Rows[0]["MAP_CODE"]);
              return nextValue.PadingWith(4);
          }

        public string Save(ACC_MAP_CLASS mapClass)
        {
            const string sp = "PKG_ACCOUNTING.acc_map_class_insert";
            string jsonStr = "{";
            var ob = mapClass;
            var i = 1;
            try
            {
              
                var ds = _db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMAP_CLASS_ID", Value = ob.MAP_CLASS_ID},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "pMAP_CODE", Value = ob.MAP_CODE},
                     new CommandParameter() {ParameterName = "pMAP_NAME", Value = ob.MAP_NAME},
                     new CommandParameter() {ParameterName = "pPRNT_CODE", Value = ob.PRNT_CODE},
                     new CommandParameter() {ParameterName = "pAC_CODE", Value = ob.AC_CODE},
                     new CommandParameter() {ParameterName = "pMAIN_CODE", Value = ob.MAIN_CODE},
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


        public bool IsMapClassExist(string compCode, string mapname,long id)
        {
            string sql = String.Format(@"select count(*) from ACC_MAP_CLASS  WHERE INSTR(UPPER(replace (MAP_NAME,' ','')), UPPER(replace ('{0}',' ',''))) >0  and COMP_CODE='{1}' and MAP_CLASS_ID<>'{2}'  ", mapname, compCode, id);
            return _db.Exist(sql);
        }

        public ACC_SUB_CLASSModel GetTreeEx()
        {
            string sql = string.Format("select * from ACC_TREE_EX");
            DataTable dataTable = _db.ExecWithSqlQuery(sql);
            string mcode = Convert.ToString(dataTable.Rows[0]["MAP_CODE"]);
            string accode = Convert.ToString(dataTable.Rows[0]["AC_CODE"]);
            string main = Convert.ToString(dataTable.Rows[0]["MAIN_CODE"]);
           return new ACC_SUB_CLASSModel { AC_CODE = accode, MAIN_CODE = main, MAP_CODE = mcode };
        }
    }
}
