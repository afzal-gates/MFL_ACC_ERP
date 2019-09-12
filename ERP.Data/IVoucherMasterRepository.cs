using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface IVoucherMasterRepository
    {
        DataTable GetVoucherMasters(string searchRefNo,string searchVhcNo,DateTime? searchDate, int pn, int ps, out int total);
        ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id,int userId);
        string GetNewPostID(string comp_code);
        ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail);
        List<ACC_TEMP_VOUCHER_DETAIL> GetTempVoucheDetail(string userId, string compCode);
        ACC_VOUCHER_MASTER SaveVoucherMaster(int voucherMasterId,ACC_VOUCHER_MASTER model);
        bool DeleteVoucherMaster(int id,string userId);
        bool DeleteTemVoucheDetail(int id, string userId);
        string GetLastDescription(string comp_code, string userId);
        bool IsVoucherNoExist(string compCode, long voucherTypeId, string voucherNo,long vId);
        string GetLastNarration(string empId, string compCode, string accountCode);
        bool CheckGlTransactionExist(string compCode, string glCode);
        DataTable GetBankReconsileVouchers(string userId, string comp_code, DateTime? fromDate, DateTime? toDate,string accountHead, int xStatus);
        int UpdateBankDate(string comp_code, int id, DateTime? bankDate);
        double GetBankReconsileBanalce(string userId, string comp_code, DateTime? fromDate, DateTime? toDate, string accountHead);
        double GetBookBanalce(string userId, string comp_code, DateTime? fromDate, DateTime? toDate, string accountHead);
        DataTable GetPandingBills(string comp_code, string ac_code, string main_code,string sub_code,DateTime?fromDate,DateTime?toDate);
        DataTable GetPaymentModes(string comp_code, Int64 voucherTypeId);
        int UpdateBillKey(string cOMP_CODE);
    }
}
