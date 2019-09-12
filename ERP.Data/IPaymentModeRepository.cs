using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface IPaymentModeRepository 
    {
        List<ACC_PAYMENT_MODE> GetPayementModes(string searchKey);
        ACC_PAYMENT_MODE GetPaymentModeById(int id);
        string GetNewRefCode(string comp_code);
        bool SavePaymentMode(int id, ACC_PAYMENT_MODE model);
        bool DeletePaymentMode(int id);
        List<ACC_PAYMENT_MODE> GetPaymentModes(string comp_code);
    }
}
