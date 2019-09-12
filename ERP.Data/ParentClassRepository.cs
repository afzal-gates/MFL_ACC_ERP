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
    public class ParentClassRepository : IParentClassRepository
    {

        private readonly OraDatabase db;
        public ParentClassRepository(OraDatabase db)
        {
            this.db = db;
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }

        public List<ACC_AC_PARENT_CLASSModel> GetAll(string compId)
        {
            string sql = String.Format("select * from ACC_AC_PARENT_CLASS where COMP_CODE='{0}'",compId);
            try
            {
                var obList = new List<ACC_AC_PARENT_CLASSModel>();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_AC_PARENT_CLASSModel ob = new ACC_AC_PARENT_CLASSModel();
                    ob.AC_PARENT_CLASS_ID = (dr["AC_PARENT_CLASS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AC_PARENT_CLASS_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.NAME = (dr["NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NAME"]);
                    ob.PARENT_CODE = (dr["PARENT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARENT_CODE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_AC_PARENT_CLASSModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public string Save(ACC_AC_PARENT_CLASSModel vtm)
        {
            throw new NotImplementedException();
        }
    }
}
