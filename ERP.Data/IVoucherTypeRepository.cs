using ERP.Model;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public interface IVoucherTypeRepository
    {
        List<ACC_VOUCHER_TYPEModel> GetAll();
        ACC_VOUCHER_TYPEModel GetById(int id);
        string Save(ACC_VOUCHER_TYPEModel vtm);
        string Delete();
        List<SelectModel> GetVoucherTypeSelectModels(string comp_code);

        bool DeleteVoucherType(int id);
    }
}
