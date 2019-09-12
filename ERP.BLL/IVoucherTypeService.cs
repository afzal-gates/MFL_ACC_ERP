using ERP.Model;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public interface IVoucherTypeService
    {
        List<ACC_VOUCHER_TYPEModel> GetVoucherTypes();
        ACC_VOUCHER_TYPEModel GetVoucerType(int id);
        string SaveVoucherType(ACC_VOUCHER_TYPEModel vtm);
        string DeleteVoucherType();
        List<SelectModel> GetVoucherTypeSelectModels(string comp_code);

        bool DeleteVoucherType(int id);
    }
}
