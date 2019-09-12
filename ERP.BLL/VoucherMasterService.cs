using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public class VoucherMasterService : IVoucherMasterService
    {
        private readonly IVoucherMasterRepository _voucherMasterRepository;
        public VoucherMasterService(IVoucherMasterRepository voucherMasterRepository)
        {
            this._voucherMasterRepository = voucherMasterRepository;
        }


        public ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id, int userId)
        {
            ACC_VOUCHER_MASTER vm = new ACC_VOUCHER_MASTER();
            if (string.IsNullOrEmpty(id))
            {
                vm.POST_ID = _voucherMasterRepository.GetNewPostID(comp_code);
                vm.VOUCHER_TYPE_ID = 1;
                vm.DESCRIPTION = _voucherMasterRepository.GetLastDescription(comp_code,userId.ToString());

            }
            else
            {
                vm = _voucherMasterRepository.GetVoucherMaster(comp_code, id, userId);
            }

            return vm;
        }

        public dynamic GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate, int pn, int ps, out int total)
        {
            DataTable dataTable = _voucherMasterRepository.GetVoucherMasters(searchRefNo, searchVhcNo, searchDate, pn, ps, out total);
            return dataTable.ToJson();
        }

        public ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail)
        {
           const int bill_voucher_type = 10;
            if (voucheDetail.COST_CENTER_ID <= 0)
            {
                throw new MultiTexInvalidDataException("Missing Cost Center ! Please Select Cost Center!");
            }
            if (voucheDetail.VOUCHER_TYPE_ID == bill_voucher_type && String.IsNullOrEmpty(voucheDetail.BILL_NO) && (voucheDetail.AC_CODE=="03" || voucheDetail.AC_CODE=="05"))
            {
                throw new MultiTexInvalidDataException("Missing Bill No ! Please Enter valid Bill No!");
            }
            ACC_TEMP_VOUCHER_DETAIL vd = _voucherMasterRepository.SaveTemVoucheDetail(voucheDetail);
            return vd;
        }

        public List<ACC_TEMP_VOUCHER_DETAIL> GetTempVoucheDetail(string userId, string compCode)
        {
            List<ACC_TEMP_VOUCHER_DETAIL> vtds = _voucherMasterRepository.GetTempVoucheDetail(userId, compCode);
            return vtds;
        }

        public ACC_VOUCHER_MASTER SaveVoucherMaster(int voucherMasterId, ACC_VOUCHER_MASTER model)
        {
            if (voucherMasterId == 0)
            {
                model.POST_ID = _voucherMasterRepository.GetNewPostID(model.COMP_CODE);
            }
            bool isExist = _voucherMasterRepository.IsVoucherNoExist(model.COMP_CODE, model.VOUCHER_TYPE_ID, model.VOUCHER_NO, voucherMasterId);
            if (isExist)
            {
                throw new MultiTexInvalidDataException("Voucher No :" + model.VOUCHER_NO + " already exist !");

            }
            else
            {
        
               ACC_VOUCHER_MASTER vtds = _voucherMasterRepository.SaveVoucherMaster(voucherMasterId, model);
              
                return new ACC_VOUCHER_MASTER();

            }
        }

        public bool DeleteVoucherMaster(int id, string userId)
        {
            bool isDelete = _voucherMasterRepository.DeleteVoucherMaster(id, userId);
            return isDelete;
        }

        public bool DeleteTemVoucheDetail(int id, string userId)
        {
            bool isDelete = _voucherMasterRepository.DeleteTemVoucheDetail(id, userId);
            return isDelete;
        }

        public string GetLastNarration(string empId, string compCode, string accountCode)
        {
            return _voucherMasterRepository.GetLastNarration(empId, compCode, accountCode);
        }

        public object GetBankReconsileVouchers(string userId, string comp_code, DateTime? fromDate, DateTime? toDate, string accountHead,int xStatus)
        {
            DataTable dataTable = _voucherMasterRepository.GetBankReconsileVouchers(userId, comp_code, fromDate, toDate, accountHead, xStatus);
            double BANK_BALANCE = _voucherMasterRepository.GetBankReconsileBanalce(userId, comp_code, fromDate, toDate, accountHead);
            double BOOK_BALANCE = _voucherMasterRepository.GetBookBanalce(userId, comp_code, fromDate, toDate, accountHead);
            object result = new
            {
                VOUCHERAS = dataTable.ToJson(),
                BANK_BALANCE,
                BOOK_BALANCE,
                BLANCE_DIFF = BOOK_BALANCE - BANK_BALANCE
            };
            return result;

        }

        public bool UpdateBankDate(string comp_code, int id, DateTime? bankDate)
        {
            int saved = _voucherMasterRepository.UpdateBankDate(comp_code, id, bankDate);
            return saved > 0;
        }

        public object GetPandingBills(string comp_code, string ac_code, string main_code, string sub_code,DateTime?fromDate,DateTime?toDate)
        {
            DataTable dataTable = new DataTable();
            if (!string.IsNullOrEmpty(ac_code))
            {
                dataTable = _voucherMasterRepository.GetPandingBills(comp_code, ac_code, main_code, sub_code,fromDate,toDate);

            }
            return dataTable.ToJson();

        }

        public object GetPaymentModes(string comp_code, Int64 voucherTypeId)
        {
            DataTable dataTable = new DataTable();
            dataTable = _voucherMasterRepository.GetPaymentModes(comp_code, voucherTypeId);
            return dataTable.ToJson();
        }

        public string GetLastNarration(string comp_code, string userId)
        {
           return _voucherMasterRepository.GetLastDescription(comp_code,userId);
        }
    }
}
