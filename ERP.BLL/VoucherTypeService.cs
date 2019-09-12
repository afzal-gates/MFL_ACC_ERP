using ERP.DAL;
using ERP.Data;
using ERP.Model;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Data;

namespace ERP.BLL
{
    public class VoucherTypeService : IVoucherTypeService
    {
       private readonly IVoucherTypeRepository voucherTypeRepository;
        public VoucherTypeService(IVoucherTypeRepository voucherTypeRepository)
        {
            this.voucherTypeRepository = voucherTypeRepository;
        }

        public string DeleteVoucherType()
        {
            throw new NotImplementedException();
        }

        public ACC_VOUCHER_TYPEModel GetVoucerType(int id)
        {
            if (id <= 0)
            {
                return new ACC_VOUCHER_TYPEModel();
            }
            ACC_VOUCHER_TYPEModel model= voucherTypeRepository.GetById(id);
            if (model == null)
            {
                throw new MultiTexInvalidDataException("Invalid Request");
            }
            return model;


        }
        public List<ACC_VOUCHER_TYPEModel> GetVoucherTypes()
        {
            return voucherTypeRepository.GetAll();
        }

        public List<SelectModel> GetVoucherTypeSelectModels(string comp_code)
        {
            return voucherTypeRepository.GetVoucherTypeSelectModels(comp_code);
        }

        public string SaveVoucherType(ACC_VOUCHER_TYPEModel model)
        {
            return voucherTypeRepository.Save(model);
        }


        public bool DeleteVoucherType(int id)
        {
            return voucherTypeRepository.DeleteVoucherType(id);
        }
    }
}
