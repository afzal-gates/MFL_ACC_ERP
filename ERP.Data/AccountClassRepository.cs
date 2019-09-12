using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public class AccountClassRepository : IAccountClassRepository
    {

        private readonly OraDatabase db;
        public AccountClassRepository(OraDatabase db)
        {
            this.db = db;
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }

        public List<ACC_AC_CLASSModel> GetAll(string compId, string acgroup)
        {
            string sp = "Select_ACC_AC_CLASS";
            try
            {
                var obList = new List<ACC_AC_CLASSModel>();
                string sql = String.Format("select * from ACC_AC_CLASS where COMP_CODE='{0}' and AC_GROUP='{1}' ", compId, acgroup);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_AC_CLASSModel ob = new ACC_AC_CLASSModel();
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.CLASS_NAME = (dr["CLASS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CLASS_NAME"]);
                    ob.AC_GROUP = (dr["AC_GROUP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_GROUP"]);
                    ob.AC_CLASS_ID = (dr["AC_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AC_CLASS_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_AC_CLASSModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public string Save(ACC_AC_CLASSModel vtm)
        {
            throw new NotImplementedException();
        }
    }
}
