using ERP.DAL;
using System;
using System.Collections.Generic;
using System.Data;

namespace ERP.Data
{
    public class ReportRepository : IReportRepository
    {

        private readonly OraDatabase db;
        public ReportRepository(OraDatabase db)
        {
            this.db = db;
        }

        public System.Data.DataTable GetVoucherReport(int id,string compCode)
        {
           
            string sp = "PKG_ACCOUNTING.acc_voucher_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value =id},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }


        public DataTable GetLedgerReport( string accountHead, DateTime fromdate, DateTime todate,string userId,string copmCode)
        {

            string sp = "PKG_ACCOUNTING.acc_Ledger_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =copmCode},
                     new CommandParameter() {ParameterName = "pGL_CODE", Value =accountHead},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetControlLedgerReport(string mainHead, DateTime fromdate, DateTime todate, string userId, string compCode)
        {

            string sp = "PKG_ACCOUNTING.acc_control_Ledger_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pACMAIN_CODE", Value =mainHead},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTrialBalanceReport(DateTime fromdate, DateTime todate, string userId, string compCode)
        {

            string sp = "PKG_ACCOUNTING.acc_trial_balance_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetVoucherSummaryReportData(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_Voucher_Summary_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }


            
        }

        public DataTable GetVoucherStatementReportData(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_Voucher_Statement_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetChartOfAccount(string userId, string compCode)
        {

            string sp = "PKG_ACCOUNTING.acc_Chart_Of_Account";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                    
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCashBookReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_cash_book_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pGL_CODE", Value =accountHead},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBankBookReport(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_bank_book_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pGL_CODE", Value =accountHead},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDraftVoucherReport(int id, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_voucher_draft_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value =id},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
      
        }

        public DataTable GetDayBookReport(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_daybook_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetReceivePaymentData(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_receive_payment_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBalanceSheetReport(DateTime fromdate, DateTime todate, string userId, string compCode)
        {
           string sp = "PKG_ACCOUNTING.acc_balancesheet_report";
           
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIncomeStatementReport(DateTime fromdate, DateTime todate, string userId, string compCode,int reportId)
        {
            string sp = "";
            switch (reportId)
            {
                case 1:
                    sp = "PKG_ACCOUNTING.acc_inc_summary_report";
                    break;
                case 2:
                    sp = "PKG_ACCOUNTING.acc_inc_detail_report";
                    break;
            }

            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLedgerCostCenterReport(string accountHead, DateTime fromdate, DateTime todate, string userId,
            string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_LEDGER_COST_CNTR_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pGL_CODE", Value =accountHead},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCostCenterLedgerReport(int costCenterId, DateTime fromdate, DateTime todate, string userId, string compCode)
        {

            string sp = "PKG_ACCOUNTING.acc_COST_CNTR_LEDGER_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pCOST_CENTER_ID", Value =costCenterId},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBankBookWithCurrecny(string accountHead, DateTime fromdate, DateTime todate, string userId, string compCode,
            int currencyId)
        {
            string sp = "PKG_ACCOUNTING.acc_bank_book_currency_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pGL_CODE", Value =accountHead},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pCURRENCY_ID", Value =currencyId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetChequeReport(int id, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_cheque_report";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value =id},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCostCenterGroupLedgerReport(int groupId, DateTime fromdate, DateTime todate, string userId, object compCode)
        {

            string sp = "PKG_ACCOUNTING.ACC_COST_CNTR_GROUP_LGR_RPT";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pGROUP_ID", Value =groupId},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pFROM_DATE", Value =fromdate},
                     new CommandParameter() {ParameterName = "pTO_DATE", Value =todate}
                 }, sp);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public DataTable GetPaidBillsReport(string comp_code, string ac_code, string main_code, string sub_code)
        {

            string sp = "PKG_ACCOUNTING.acc_paid_bills_report";
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =comp_code},

                     new CommandParameter() {ParameterName = "pAC_CODE", Value =ac_code},
                     new CommandParameter() {ParameterName = "pMAIN_CODE", Value =main_code},
                     new CommandParameter() {ParameterName = "pSUB_CODE", Value =sub_code},
                 }, sp);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];

            }

            return ds.Tables[0];
        }

        public DataTable GetDuplicateBillPaymentReport(string comp_code)
        {
            string sp = "PKG_ACCOUNTING.acc_duplicate_bills_report";
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =comp_code},

                 }, sp);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];

            }

            return ds.Tables[0];
        }
    }
}
