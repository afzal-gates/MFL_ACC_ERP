using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public class DraftVoucherService : IDraftVoucherService
    {
        private readonly IDraftVoucherRepository _voucherMasterRepository;
        public DraftVoucherService(IDraftVoucherRepository voucherMasterRepository)
        {
            this._voucherMasterRepository = voucherMasterRepository;
        }

      
        public ACC_VOUCHER_MASTER GetVoucherMaster(string comp_code, string id,int userId)
        {
            ACC_VOUCHER_MASTER vm = new ACC_VOUCHER_MASTER();
            if (string.IsNullOrEmpty(id))
            {
                vm.POST_ID = _voucherMasterRepository.GetNewPostID(comp_code);
                vm.VOUCHER_TYPE_ID = 1;
                vm.DESCRIPTION = _voucherMasterRepository.GetLastDescription(userId);
            }
            else
            {
                vm = _voucherMasterRepository.GetVoucherMaster(comp_code, id, userId);
            }
     
            return vm;
        }

        public dynamic GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate,int pn,int ps,out int total)
        {
            DataTable dataTable = _voucherMasterRepository.GetVoucherMasters(searchRefNo, searchVhcNo, searchDate, pn, ps, out  total);
            return dataTable.ToJson();
        }

        public ACC_TEMP_VOUCHER_DETAIL SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail)
        {
            ACC_TEMP_VOUCHER_DETAIL vd =  _voucherMasterRepository.SaveTemVoucheDetail(voucheDetail);
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
                throw  new MultiTexInvalidDataException("Voucher No :"+model.VOUCHER_NO +" already exist !");
              
            }
            else
            {
                ACC_VOUCHER_MASTER vtds = _voucherMasterRepository.SaveVoucherMaster(voucherMasterId, model);
                return vtds;
                
            }
           
        }

        public bool DeleteVoucherMaster(int id,string userId)
        {
            bool isDelete = _voucherMasterRepository.DeleteVoucherMaster(id, userId);
            return isDelete;
        }

        public bool DeleteTemVoucheDetail(int id, string userId)
        {
            bool isDelete = _voucherMasterRepository.DeleteTemVoucheDetail(id,userId);
            return isDelete;
        }

        public string GetLastNarration(string userId ,string compCode, string accountCode)
        {
            return _voucherMasterRepository.GetLastNarration(userId, compCode, accountCode); 
        }
    }
}
