using ERP.DAL;
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
   public class CompanyRepository: ICompanyRepository
    {
        private readonly OraDatabase db;
        public CompanyRepository(OraDatabase db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            string sql = String.Format(@"delete from ACC_COMPANY where COMPANY_ID='{0}'", id);
            return db.ExecNoneQuery(sql) > 0;
        }

        public List<ACC_COMPANY> GetAll()
        {
            try
            {
                var obList = new List<ACC_COMPANY>();
                string sql = String.Format("select * from  ACC_COMPANY");
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_COMPANY ob = new ACC_COMPANY();
                    ob.COMPANY_ID = (dr["COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COMPANY_ID"]);
                    ob.COMP_NAME = (dr["COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.DESCRIPTION = (dr["DESCRIPTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESCRIPTION"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.PREFIX = (dr["PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PREFIX"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_COMPANY GetById(int id)
        {
            string sql = String.Format("select * from  ACC_COMPANY where COMPANY_ID='{0}'", id);
            try
            {
                var ob = new ACC_COMPANY();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                  
                    ob.COMPANY_ID = (dr["COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COMPANY_ID"]);
                    ob.COMP_NAME = (dr["COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.DESCRIPTION = (dr["DESCRIPTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESCRIPTION"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.PREFIX = (dr["PREFIX"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PREFIX"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetNewId()
        {
            string sql = string.Format("select NVL(MAX(COMP_CODE),'000') as COMP_CODE  from ACC_COMPANY ");
            DataTable dataTable = db.ExecWithSqlQuery(sql);
            string nextValue = Convert.ToString(dataTable.Rows[0]["COMP_CODE"]);
            return nextValue.PadingWith(3);
        }
        public bool Save(int id, ACC_COMPANY model)
        {
            string sql = "";
            if (id > 0)
            {
                sql = String.Format(@"update ACC_COMPANY SET COMP_NAME='{0}',
                                    DESCRIPTION= '{1}' where COMPANY_ID='{2}'",
                                    model.COMP_NAME, model.DESCRIPTION, model.COMPANY_ID);
            }
            else
            {
                sql = String.Format(@"Insert Into ACC_COMPANY (COMPANY_ID,COMP_NAME,COMP_CODE,DESCRIPTION,PREFIX)
                    Values (acc_company_seq.nextval,'{0}','{1}','{2}','{3}')",
                    model.COMP_NAME, model.COMP_CODE,
                     model.DESCRIPTION, model.PREFIX);
            }
            return db.ExecNoneQuery(sql) > 0;
        }
    }
}
