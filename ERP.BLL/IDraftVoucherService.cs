using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public interface IDraftVoucherService
    {
        dynamic GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate,int pn,int ps,out int total);
        ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id, int userId);
        ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail);
        List<ACC_TEMP_VOUCHER_DETAIL> GetTempVoucheDetail(string userId, string compCode);
        ACC_VOUCHER_MASTER SaveVoucherMaster(int v, ACC_VOUCHER_MASTER model);
        bool DeleteVoucherMaster(int id, string userId);
        bool DeleteTemVoucheDetail(int id, string userId);
        string GetLastNarration(string userId, string compCode, string accountCode);
    }
}
