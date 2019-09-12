using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public interface IPaymentModeService
    {
        bool SavePaymentMode(int id, ACC_PAYMENT_MODE model);
        ACC_PAYMENT_MODE GetPaymentModeById(int id);
        bool DeletePaymentMode(int id);
        List<ACC_PAYMENT_MODE> GetPayementModes(string searchKey);
        List<ACC_PAYMENT_MODE> GetPaymentModes(string comp_code);
    }
}
