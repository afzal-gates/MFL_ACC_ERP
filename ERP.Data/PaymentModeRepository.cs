using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.DAL;
using ERP.Model;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.Data
{
    public class PaymentModeRepository : IPaymentModeRepository
    {
        private readonly OraDatabase _db;
        public PaymentModeRepository(OraDatabase db)
        {
            this._db = db;
        }

        public bool DeletePaymentMode(int id)
        {
            string sp = "PKG_ACCOUNTING.ac_payment_mode_delete";
             var ds = _db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value = id},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

            return true;

        }

        public string GetNewRefCode(string comp_code)
        {
            string sql = string.Format("select NVL(MAX(REF_CODE),'000') as REF_CODE  from acc_payment_mode where COMP_CODE='{0}'", comp_code);
            DataTable dataTable = _db.ExecWithSqlQuery(sql);
            string nextValue = Convert.ToString(dataTable.Rows[0]["REF_CODE"]);
            return nextValue.PadingWith(3);
        }

        public List<ACC_PAYMENT_MODE> GetPayementModes(string searchKey)
        {
            string sp = "PKG_ACCOUNTING.ac_payment_mode_select";
            try
            {
                var obList = new List<ACC_PAYMENT_MODE>();
                var ds = _db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSEARCH_KEY", Value =searchKey},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_PAYMENT_MODE ob = new ACC_PAYMENT_MODE();
                    ob.PAYMENT_MODE_ID = (dr["PAYMENT_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAYMENT_MODE_ID"]);
                    ob.PM_NAME = (dr["PM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PM_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REF_CODE = (dr["REF_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_CODE"]);
                    ob.SHORT_NAME = (dr["SHORT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHORT_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_PAYMENT_MODE GetPaymentModeById(int id)
        {
            string sp = "PKG_ACCOUNTING.ac_payment_mode_select_by_Id";
            try
            {
                ACC_PAYMENT_MODE ob = new ACC_PAYMENT_MODE();
                var ds = _db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value =id},
                 }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.PAYMENT_MODE_ID = (dr["PAYMENT_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAYMENT_MODE_ID"]);
                    ob.PM_NAME = (dr["PM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PM_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REF_CODE = (dr["REF_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_CODE"]);
                    ob.SHORT_NAME = (dr["SHORT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHORT_NAME"]);
                  
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ACC_PAYMENT_MODE> GetPaymentModes(string comp_code)
        {
            string sp = "PKG_ACCOUNTING.ac_payment_mode_select";
            try
            {
                var obList = new List<ACC_PAYMENT_MODE>();
                DataTable dataTable = _db.ExecWithSqlQuery(String.Format("SELECT * FROM ACC_PAYMENT_MODE WHERE COMP_CODE={0}", comp_code));
                foreach (DataRow dr in dataTable.Rows)
                {
                    ACC_PAYMENT_MODE ob = new ACC_PAYMENT_MODE();
                    ob.PAYMENT_MODE_ID = (dr["PAYMENT_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAYMENT_MODE_ID"]);
                    ob.PM_NAME = (dr["PM_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PM_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.REF_CODE = (dr["REF_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_CODE"]);
                    ob.SHORT_NAME = (dr["SHORT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHORT_NAME"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
       
        }

        public bool SavePaymentMode(int id, ACC_PAYMENT_MODE model)
        {
            const string sp = "PKG_ACCOUNTING.acc_payment_mode_insert";
            var ob = model;
    
            try
            {
                var ds = _db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value = id},
                     new CommandParameter() {ParameterName = "pPM_NAME", Value = ob.PM_NAME},
                      new CommandParameter() {ParameterName = "pREF_CODE", Value = ob.REF_CODE},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pSHORT_NAME", Value = ob.SHORT_NAME},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                return true;
            }
            catch (Exception ex)
            {
                throw new MultiTexDatabaseException("Payment mode not save interla save error please contact with provider {0}", ex.Message);
            }
        }
    }
}
