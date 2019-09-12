using ERP.DAL;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace ERP.Data
{
    public class StockClosingRepository : IStockClosingRepository
    {

        private readonly OraDatabase db;
        public StockClosingRepository(OraDatabase db)
        {
            this.db = db;
        }

        public List<ACC_STOCK_CLOSING> GetMonthlyStockClosing(int month_code, int year_code)
        {
            string sp = "PKG_ACCOUNTING.acc_stock_closing_select";
            try
            {
                List<ACC_STOCK_CLOSING> stocks = new List<ACC_STOCK_CLOSING>();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pYEAR_CODE", Value =year_code},
                     new CommandParameter() {ParameterName = "pMONTH_CODE", Value =month_code},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =CompanyCode.comp_code},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                int index = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    index += 1;
                    var ob = new ACC_STOCK_CLOSING();
                    ob.STOCK_CLOSING_ID = index;
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.SUB_CODE = (dr["SUB_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_CODE"]);
                    ob.SUB_NAME = (dr["SUB_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_NAME"]);
                    ob.OPENING = (dr["OPENING"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["OPENING"]);
                    ob.DR = (dr["DR"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["DR"]);
                    ob.CR = (dr["CR"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["CR"]);
                    ob.CLOSING = (dr["CLOSING"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["CLOSING"]);
                    ob.BALANCE = (dr["BALANCE"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["BALANCE"]);
                    ob.NET_AMT = (dr["NET_AMT"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["NET_AMT"]);
                    ob.YEAR_CODE = (dr["YEAR_CODE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YEAR_CODE"]);
                    ob.MONTH_CODE = (dr["MONTH_CODE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MONTH_CODE"]);

                    stocks.Add(ob);
                }
                return stocks;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveMonthlyStockClosing(ACC_STOCK_CLOSING model)
        {
            int effectedRows = 0;
            string sqlQuery = "delete ACC_STOCK_HISTORY where  YEAR_CODE='{0}' and  MONTH_CODE='{1}' and AC_CODE='{2}' and MAIN_CODE='{3}' and SUB_CODE='{4}'";
            sqlQuery = string.Format(sqlQuery, model.YEAR_CODE, model.MONTH_CODE, model.AC_CODE, model.MAIN_CODE, model.SUB_CODE);
            effectedRows = db.ExecNoneQuery(sqlQuery);

            sqlQuery = @"insert into ACC_STOCK_HISTORY ( STOCK_HISTORY_ID, AC_CODE,MAIN_CODE,SUB_CODE,CLOSING,YEAR_CODE,MONTH_CODE,COMP_CODE) VALUES ( ACC_STOCK_CLOSING_SEQ.nextval,'{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
            sqlQuery = string.Format(sqlQuery, model.AC_CODE, model.MAIN_CODE, model.SUB_CODE, model.CLOSING, model.YEAR_CODE, model.MONTH_CODE,model.COMP_CODE);
            effectedRows += db.ExecNoneQuery(sqlQuery);

            return effectedRows;
        }

        public int UpdateMonthlyStockClosing(ACC_VOUCHER_MASTER vm,int year_code,int month_code)
        {

            const string sp = "PKG_ACCOUNTING.acc_stor_closing_transaction";
            var ob = vm;
            try
            {

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pYEAR_CODE", Value = year_code},
                     new CommandParameter() {ParameterName = "pMONTH_CODE", Value = month_code},
                     new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value = ob.PAYMENT_MODE_ID},
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value = ob.VOUCHER_MASTER_ID},
                     new CommandParameter() {ParameterName = "pPOST_ID", Value = ob.POST_ID},
                     new CommandParameter() {ParameterName = "pPOST_DATE", Value = ob.POST_DATE},
                     new CommandParameter() {ParameterName = "pVOUCHER_NO", Value = ob.VOUCHER_NO},
                     new CommandParameter() {ParameterName = "pVOUCHER_TYPE_ID", Value = ob.VOUCHER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDESCRIPTION", Value = ob.DESCRIPTION},
                     new CommandParameter() {ParameterName = "pEMPLOYEE_ID", Value = ob.EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pREF_ID", Value = ob.REF_ID},
                     new CommandParameter() {ParameterName = "pDIN", Value = ob.DIN},
                     new CommandParameter() {ParameterName = "pCHQ_NO", Value = ob.CHQ_NO},
                     new CommandParameter() {ParameterName = "pCHQ_DATE", Value = ob.CHQ_DATE},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateMonthlyStockOpening(ACC_VOUCHER_MASTER vm, int year_code, int month_code)
        {

            const string sp = "PKG_ACCOUNTING.acc_stor_opening_transaction";
            var ob = vm;
            try
            {

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pYEAR_CODE", Value = year_code},
                     new CommandParameter() {ParameterName = "pMONTH_CODE", Value = month_code},
                     new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value = ob.PAYMENT_MODE_ID},
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value = ob.VOUCHER_MASTER_ID},
                     new CommandParameter() {ParameterName = "pPOST_ID", Value = ob.POST_ID},
                     new CommandParameter() {ParameterName = "pPOST_DATE", Value = ob.POST_DATE},
                     new CommandParameter() {ParameterName = "pVOUCHER_NO", Value = ob.VOUCHER_NO},
                     new CommandParameter() {ParameterName = "pVOUCHER_TYPE_ID", Value = ob.VOUCHER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pDESCRIPTION", Value = ob.DESCRIPTION},
                     new CommandParameter() {ParameterName = "pEMPLOYEE_ID", Value = ob.EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pREF_ID", Value = ob.REF_ID},
                     new CommandParameter() {ParameterName = "pDIN", Value = ob.DIN},
                     new CommandParameter() {ParameterName = "pCHQ_NO", Value = ob.CHQ_NO},
                     new CommandParameter() {ParameterName = "pCHQ_DATE", Value = ob.CHQ_DATE},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
