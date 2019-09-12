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
    public class CostCneterGroupRepository : ICostCneterGroupRepository
    {
        private readonly OraDatabase db;
        public CostCneterGroupRepository(OraDatabase db)
        {
            this.db = db;
        }

        public List<ACC_COST_CENTER_GROUP> GetAll(string compCode)
        {
            try
            {
                var obList = new List<ACC_COST_CENTER_GROUP>();
                string sql = String.Format("select * from ACC_COST_CENTER_GROUP where COMP_CODE='{0}'", compCode);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_COST_CENTER_GROUP ob = new ACC_COST_CENTER_GROUP();
                    ob.GROUP_ID = (dr["GROUP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GROUP_ID"]);
                    ob.NAME = (dr["NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_COST_CENTER_GROUP GetById(int id)
        {
            string sql = String.Format("select * from ACC_COST_CENTER_GROUP where GROUP_ID='{0}'", id);
            try
            {
                ACC_COST_CENTER_GROUP ob = new ACC_COST_CENTER_GROUP();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
 
                    ob.GROUP_ID = (dr["GROUP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GROUP_ID"]);
                    ob.NAME = (dr["NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                  

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Save(int id, ACC_COST_CENTER_GROUP model)
        {
            string sql = "";
            if (id > 0)
            {
                sql = String.Format(@"update ACC_COST_CENTER_GROUP SET NAME='{0}',
                                    REMARKS= '{1}' where GROUP_ID='{2}'",
                                    model.NAME, model.REMARKS, model.GROUP_ID);
            }
            else
            {
                sql = String.Format(@"Insert Into ACC_COST_CENTER_GROUP (GROUP_ID,NAME,COMP_CODE,REMARKS)
                    Values (acc_cost_center_group_seq.nextval,'{0}','{1}','{2}')",
                    model.NAME, model.COMP_CODE,
                    model.REMARKS);
            }
            return db.ExecNoneQuery(sql) > 0;
        }

        public bool Delete(int id)
        {
            string sql = string.Format(@"delete from ACC_COST_CENTER_GROUP where GROUP_ID='{0}'", id);
            return db.ExecNoneQuery(sql) > 0;
        }
    }
}
