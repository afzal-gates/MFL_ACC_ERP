using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public interface IVoucherMasterService
    {
        dynamic GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate, int pn, int ps, out int total);
        ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id, int userId);
        ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail);
        List<ACC_TEMP_VOUCHER_DETAIL> GetTempVoucheDetail(string userId, string compCode);
        ACC_VOUCHER_MASTER SaveVoucherMaster(int v, ACC_VOUCHER_MASTER model);
        bool DeleteVoucherMaster(int id, string userId);
        bool DeleteTemVoucheDetail(int id, string userId);
        string GetLastNarration(string userId, string compCode, string accountCode);
        object GetBankReconsileVouchers(string userId, string comp_code, DateTime? fromDate, DateTime? toDate,string accountHead,int xStatus);
        bool UpdateBankDate(string comp_code, int id, DateTime? bankDate);
        object GetPandingBills(string comp_code, string ac_code, string main_code, string sub_code,DateTime?fromDate,DateTime?toDate);
        object GetPaymentModes(string comp_code, Int64 voucherTypeId);
        string GetLastNarration(string comp_code, string userId);
    }
}
