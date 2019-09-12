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
    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly OraDatabase db;
        public CurrencyRepository(OraDatabase db)
        {
            this.db = db;
        }
        public List<ACC_CURRENCY> GetCurrencyList(string comp_code)
        {
            try
            {
                var obList = new List<ACC_CURRENCY>();
                string sql = String.Format("select * from ACC_CURRENCY where COMP_CODE='{0}'", comp_code);
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_CURRENCY ob = new ACC_CURRENCY();
                    ob.CURRENCY_ID = (dr["CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CURRENCY_ID"]);
                    ob.FORMAL_NAME = (dr["FORMAL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FORMAL_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.SYMBOL = (dr["SYMBOL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SYMBOL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RATE = (dr["RATE"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["RATE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_CURRENCY GetCurrencyById(int id)
        {
            string sql = String.Format("select * from ACC_CURRENCY where CURRENCY_ID='{0}'", id);
            try
            {
                var ob = new ACC_CURRENCY();
                var ds = db.ExecuteSQLStatement(sql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CURRENCY_ID = (dr["CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CURRENCY_ID"]);
                    ob.FORMAL_NAME = (dr["FORMAL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FORMAL_NAME"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.SYMBOL = (dr["SYMBOL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SYMBOL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RATE = (dr["RATE"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["RATE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveCurrency(int id, ACC_CURRENCY model)
        {
            string sql = "";
            if (id > 0)
            {
                sql = String.Format(@"update ACC_CURRENCY SET FORMAL_NAME='{0}',
                                    REMARKS= '{1}' , RATE='{2}', SYMBOL='{3}' where CURRENCY_ID='{4}'",
                                    model.FORMAL_NAME, model.REMARKS,model.RATE,model.SYMBOL, model.CURRENCY_ID);
            }
            else
            {
                sql = String.Format(@"Insert Into ACC_CURRENCY (CURRENCY_ID,SYMBOL,FORMAL_NAME,RATE,REMARKS,COMP_CODE)
                    Values (acc_currency_seq.nextval,'{0}','{1}','{2}','{3}','{4}')",
                    model.SYMBOL,model.FORMAL_NAME,model.RATE,model.REMARKS, model.COMP_CODE);
            }
            return db.ExecNoneQuery(sql) > 0;
        }

        public bool DeleteCurrency(int id)
        {
            string sql = string.Format(@"delete from ACC_CURRENCY where CURRENCY_ID='{0}'", id);
            return db.ExecNoneQuery(sql) > 0;
        }
    }
}
