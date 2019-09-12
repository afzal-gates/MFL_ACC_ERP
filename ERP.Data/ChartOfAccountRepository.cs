using ERP.DAL;
using ERP.Model;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private readonly OraDatabase db;
        public ChartOfAccountRepository(OraDatabase db)
        {
            this.db = db;
        }

        public string Delete()
        {
            throw new NotImplementedException();
        }

        public ACC_AC_CLASSModel GetById(int id)
        {
            return new ACC_AC_CLASSModel();
        }


        public List<ACC_AC_CLASSModel> GetAll()
        {
            string sp = "pkg_accounts.acc_ac_class_select";
            try
            {
                var obList = new List<ACC_AC_CLASSModel>();
        
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =CompanyCode.comp_code},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
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

        public string Save(ACC_AC_CLASSModel model)
        {
            return null;
        }
    }
}

    

