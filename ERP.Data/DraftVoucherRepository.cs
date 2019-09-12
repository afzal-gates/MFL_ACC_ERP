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
    public class DraftVoucherRepository : IDraftVoucherRepository
    {
        private readonly OraDatabase db;
        private readonly IVoucherMasterRepository voucherMasterRepository;
        public DraftVoucherRepository(OraDatabase db, IVoucherMasterRepository voucherMasterRepository)
        {
            this.db = db;
            this.voucherMasterRepository = voucherMasterRepository;
        }

        public string GetNewPostID(string comp_code)
        {
            string sql = string.Format("select NVL(MAX(SUBSTR(POST_ID,2,10)),'0000000000') as POST_ID  from ACC_DRAFT_VOUCHER where COMP_CODE='{0}'", comp_code);
            DataTable dataTable = db.ExecWithSqlQuery(sql);
            string nextValue = Convert.ToString(dataTable.Rows[0]["POST_ID"]);
            return nextValue.PadingWith(9, "D");
        }



        public ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id, int userId)
        {
            string sp = String.Format(@"PKG_ACCOUNTING.acc_draft_voucher_select");
            try
            {
                ACC_VOUCHER_MASTER ob = new ACC_VOUCHER_MASTER();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPOST_ID", Value =id},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =comp_code},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},


                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.VOUCHER_MASTER_ID = (dr["VOUCHER_MASTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VOUCHER_MASTER_ID"]);
                    ob.POST_ID = (dr["POST_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["POST_ID"]);
                    ob.POST_DATE = (dr["POST_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["POST_DATE"]);
                    ob.VOUCHER_NO = (dr["VOUCHER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["VOUCHER_NO"]);
                    ob.VOUCHER_TYPE_ID = (dr["VOUCHER_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VOUCHER_TYPE_ID"]);
                    ob.DESCRIPTION = (dr["DESCRIPTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESCRIPTION"]);
                    ob.EMPLOYEE_ID = (dr["EMPLOYEE_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_ID"]);
                    ob.REF_ID = (dr["REF_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_ID"]);
                    ob.DIN = (dr["DIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIN"]);
                    ob.CHQ_NO = (dr["CHQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHQ_NO"]);
                    ob.CHQ_DATE = (dr["CHQ_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHQ_DATE"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.IS_POSTED = (dr["IS_POSTED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_POSTED"]);
                    ob.PAYMENT_MODE_ID = (dr["PAYMENT_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PAYMENT_MODE_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate, int pn, int ps, out int total)
        {
            total = 0;
            string sp = "PKG_ACCOUNTING.acc_draft_voucher_paiging";
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =1},
                     new CommandParameter() {ParameterName = "pSEARCH_STRING", Value =searchRefNo},
                             new CommandParameter() {ParameterName = "pSearchVhcNo", Value =searchVhcNo},
                                     new CommandParameter() {ParameterName = "pSearchDate", Value =searchDate},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pn},
                     new CommandParameter() {ParameterName = "pageSize", Value =ps}

                 }, sp);

            if (ds.Tables[0].Rows.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];
                total = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
            }

            return ds.Tables[0];
        }

        public ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail)
        {
            const string sp = "PKG_ACCOUNTING.acc_temp_draft_voucher_insert";
            var ob = voucheDetail;
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pVUCHER_TYPE_ID", Value = ob.VOUCHER_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "pAC_CODE", Value = ob.AC_CODE},
                     new CommandParameter() {ParameterName = "pMAIN_CODE", Value = ob.MAIN_CODE},
                     new CommandParameter() {ParameterName = "pSUB_CODE", Value = ob.SUB_CODE},
                     new CommandParameter() {ParameterName = "pSUB_NAME", Value = ob.SUB_NAME},
                     new CommandParameter() {ParameterName = "pDR_AMT", Value = ob.DR_AMT},
                     new CommandParameter() {ParameterName = "pCR_AMT", Value = ob.CR_AMT},
                     new CommandParameter() {ParameterName = "pDESCRIPTION", Value = ob.DESCRIPTION},
                     new CommandParameter() {ParameterName = "pXSTATUS", Value = ob.XSTATUS},
                     new CommandParameter() {ParameterName = "pMAP_CODE", Value = ob.MAP_CODE},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = ob.USER_ID},
                     new CommandParameter() {ParameterName = "pCOST_CENTER_ID", Value = ob.COST_CENTER_ID},
                     new CommandParameter() {ParameterName = "pEXCHANGE_RATE", Value = ob.EXCHANGE_RATE},
                     new CommandParameter() {ParameterName = "pCURRENCY_ID", Value = ob.CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ACC_TEMP_VOUCHER_DETAIL> GetTempVoucheDetail(string userId, string compCode)
        {
            string sp = "PKG_ACCOUNTING.acc_temp_draft_voucher_select";

            try
            {
                var obList = new List<ACC_TEMP_VOUCHER_DETAIL>();

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value =compCode},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =userId},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ACC_TEMP_VOUCHER_DETAIL ob = new ACC_TEMP_VOUCHER_DETAIL();
                    ob.TEMP_ID = (dr["TEMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TEMP_ID"]);
                    ob.COMP_CODE = (dr["COMP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_CODE"]);
                    ob.AC_CODE = (dr["AC_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AC_CODE"]);
                    ob.MAIN_CODE = (dr["MAIN_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIN_CODE"]);
                    ob.SUB_CODE = (dr["SUB_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_CODE"]);
                    ob.SUB_NAME = (dr["SUB_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_NAME"]);
                    ob.DR_AMT = (dr["DR_AMT"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["DR_AMT"]);
                    ob.CR_AMT = (dr["CR_AMT"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["CR_AMT"]);
                    ob.DESCRIPTION = (dr["DESCRIPTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESCRIPTION"]);
                    ob.XSTATUS = (dr["XSTATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["XSTATUS"]);
                    ob.MAP_CODE = (dr["MAP_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAP_CODE"]);
                    ob.USER_ID = (dr["USER_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_ID"]);
                    ob.COST_CENTER_NAME = (dr["COST_CENTER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COST_CENTER_NAME"]);
                    ob.COST_CENTER_ID = (dr["COST_CENTER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["COST_CENTER_ID"]);
                    ob.CURRENCY_ID = (dr["CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CURRENCY_ID"]);
                    ob.EXCHANGE_RATE = (dr["EXCHANGE_RATE"] == DBNull.Value) ? 0 : Convert.ToDouble(dr["EXCHANGE_RATE"]);
                    obList.Add(ob);
                }
                return obList.OrderBy(x => x.XSTATUS).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ACC_VOUCHER_MASTER SaveVoucherMaster(int voucherMasterId, ACC_VOUCHER_MASTER model)
        {

            string sp = "PKG_ACCOUNTING.acc_draft_voucher_insert";
            var ob = model;
            try
            {

                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value = ob.PAYMENT_MODE_ID},
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value = voucherMasterId},
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
                       new CommandParameter() {ParameterName = "pIS_POSTED", Value = ob.IS_POSTED},
                        new CommandParameter() {ParameterName="pACNT_NAME", Value = ob.ACNT_NAME},
                     new CommandParameter() {ParameterName ="pPAY_TYPE", Value = ob.PAY_TYPE},
                  new CommandParameter() {ParameterName = "pPAYMENT_MODE_ID", Value = ob.PAYMENT_MODE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                if (model.IS_POSTED == "P")
                {
                    string mPOST_ID = "";
                    mPOST_ID = voucherMasterRepository.GetNewPostID(model.COMP_CODE);
                    sp = "PKG_ACCOUNTING.acc_confirm_insert";
                    ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value = ob.VOUCHER_MASTER_ID},
                     new CommandParameter() {ParameterName = "pPOST_ID", Value = ob.POST_ID},
                     new CommandParameter() {ParameterName = "pREF_ID", Value = ob.REF_ID},
                     new CommandParameter() {ParameterName = "pCOMP_CODE", Value = ob.COMP_CODE},
                     new CommandParameter() {ParameterName = "mPOST_ID", Value = mPOST_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public bool DeleteVoucherMaster(int id, string userId)
        {
            const string sp = "PKG_ACCOUNTING.acc_draft_voucher_delete";
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pVOUCHER_MASTER_ID", Value = id},
                      new CommandParameter() {ParameterName = "pUSER_ID", Value = userId},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteTemVoucheDetail(int id, string userId)
        {
            string sql = "";
            if (id > 0)
            {
                sql = string.Format("delete from ACC_TEMP_DRAFT_VOUCHER where TEMP_ID='{0}'", id);
            }
            else
            {
                sql = string.Format("delete from ACC_TEMP_DRAFT_VOUCHER where USER_ID='{0}'", userId);
            }

            return db.ExecNoneQuery(sql) > 0;
        }

        public string GetLastDescription(int userId)
        {
            string sql = string.Format("select DESCRIPTION from ACC_DRAFT_VOUCHER where EMPLOYEE_ID={0} and  ROWNUM=1 order by VOUCHER_MASTER_ID DESC", userId);
            DataTable dataTable = db.ExecWithSqlQuery(sql);
            string descr = "";
            if (dataTable.Rows.Count > 0)
            {
                descr = Convert.ToString(dataTable.Rows[0]["DESCRIPTION"]);
            }
            return descr;
        }


        public bool IsVoucherNoExist(string compCode, long vTp, string vno, long vId)
        {
            string sql = String.Format(@"select count(*) from ACC_DRAFT_VOUCHER  WHERE UPPER(replace (VOUCHER_NO,' ',''))= UPPER(replace ('{0}',' ','')) and VOUCHER_TYPE_ID='{1}' and COMP_CODE='{2}' and VOUCHER_MASTER_ID<>'{3}' ", vno, vTp, compCode, vId);
            return db.Exist(sql);
        }

        public string GetLastNarration(string userId, string compCode, string accountCode)
        {
            string sql = String.Format(@"select D.DESCRIPTION from ACC_DRAFT_VOUCHER_DETAIL D
                    inner join ACC_DRAFT_VOUCHER M on D.VOUCHER_MASTER_ID=M.VOUCHER_MASTER_ID
                    where D.AC_CODE||D.MAIN_CODE||D.SUB_CODE='{0}'  and ROWNUM=1 and M.COMP_CODE='{1}' and M.EMPLOYEE_ID='{2}' order by M.VOUCHER_MASTER_ID DESC", accountCode, compCode, userId);
            DataTable dataTable = db.ExecWithSqlQuery(sql);
            string descr = "--";
            if (dataTable.Rows.Count > 0)
            {
                descr = Convert.ToString(dataTable.Rows[0]["DESCRIPTION"]);
            }
            return descr;
        }
    }
}
