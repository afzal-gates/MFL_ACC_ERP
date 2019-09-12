using ERP.DAL;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace ERP.Data
{
    public class CostCenterRepository : ICostCenterRepository
    {
        private readonly OraDatabase db;
        public CostCenterRepository(OraDatabase db)
        {
            this.db = db;
        }

        public bool Delete(int id)
        {
            string sql = string.Format(@"delete from ACC_COST_CENTER where COST_CENTER_ID='{0}'", id);
            return db.ExecNoneQuery(sql) > 0;
        }

        public List<ACC_COST_CENTER> GetAll(string comp_code)
        {
            try
            {
                var obList = new List<ACC_COST_CENTER>();
                string sql = String.Format("select C.*," + "(select NAME from ACC_COST_CENTER_GROUP where GROUP_ID=C.GROUP_ID and ROWNUM=1) AS GR_NAME" + 
                    " from ACC_COST_CENTER C where COMP_CODE='{0}'", comp_code);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_COST_CENTER ob = new ACC_COST_CENTER();
                    ob.COST_CENTER_ID = (dr["COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COST_CENTER_ID"]);
                    ob.GROUP_ID = (dr["GROUP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["GROUP_ID"]);
                    ob.GR_NAME = (dr["GR_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["GR_NAME"]);
                    ob.COST_CENTER_NAME = (dr["COST_CENTER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CENTER_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.COST_CENTER_CODE = (dr["COST_CENTER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CENTER_CODE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_COST_CENTER GetById(int id)
        {
            string sql = String.Format(@"select * from ACC_COST_CENTER  C where COST_CENTER_ID='{0}' order by COST_CENTER_CODE", id);
            try
            {
                var ob = new ACC_COST_CENTER();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.COST_CENTER_ID = (dr["COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COST_CENTER_ID"]);
                   
                    ob.COST_CENTER_NAME = (dr["COST_CENTER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CENTER_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.COST_CENTER_CODE = (dr["COST_CENTER_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CENTER_CODE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public string GetNewId(string comp_code)
        {
            string sql = string.Format("select NVL(MAX(COST_CENTER_CODE),'000') as COST_CENTER_CODE  from ACC_COST_CENTER where COMP_CODE='{0}'", comp_code);
            DataTable dataTable = db.ExecWithSqlQuery(sql);
            string nextValue = Convert.ToString(dataTable.Rows[0]["COST_CENTER_CODE"]);
            return nextValue.PadingWith(3);
        }

        public bool Save(int id, ACC_COST_CENTER model)
        {
            string sql = "";
            if (id > 0)
            {
                sql = String.Format(@"update acc_cost_center SET COST_CENTER_NAME='{0}',
                                    REMARKS= '{1}',GROUP_ID='{2}' where COST_CENTER_ID='{3}'",
                                    model.COST_CENTER_NAME, model.REMARKS, model.GROUP_ID, model.COST_CENTER_ID);
            }
            else
            {
                sql = String.Format(@"Insert Into acc_cost_center (COST_CENTER_ID,COST_CENTER_NAME,COMP_CODE,REMARKS,COST_CENTER_CODE,GROUP_ID)
                    Values (acc_cost_center_seq.nextval,'{0}','{1}','{2}','{3}',{4})",
                    model.COST_CENTER_NAME, model.COMP_CODE,
                    model.REMARKS, model.COST_CENTER_CODE,model.GROUP_ID);
            }
            return db.ExecNoneQuery(sql) > 0;
        }

        public List<SelectModel> GetCostCenterSelectModels(string comp_code)
        {

            try
            {
                var obList = new List<SelectModel>();
                string sql = String.Format("select COST_CENTER_ID,COST_CENTER_NAME from ACC_COST_CENTER where COMP_CODE='{0}'", comp_code);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SelectModel ob = new SelectModel();
                    ob.Value = (dr["COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COST_CENTER_ID"]);
                    ob.Text = (dr["COST_CENTER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CENTER_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
