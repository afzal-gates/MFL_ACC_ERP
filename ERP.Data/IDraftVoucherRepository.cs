using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface IDraftVoucherRepository
    {
        DataTable GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate, int pn, int ps, out int total);
        ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id,int userId);
        string GetNewPostID(string comp_code);
        ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail);
        List<ACC_TEMP_VOUCHER_DETAIL> GetTempVoucheDetail(string userId, string compCode);
        ACC_VOUCHER_MASTER SaveVoucherMaster(int voucherMasterId,ACC_VOUCHER_MASTER model);
        bool DeleteVoucherMaster(int id,string userId);
        bool DeleteTemVoucheDetail(int id, string userId);
        string GetLastDescription(int userId);

        bool IsVoucherNoExist(string compCode, long vTp, string vno, long vId);
        string GetLastNarration(string userId, string compCode, string accountCode);
    }
}
