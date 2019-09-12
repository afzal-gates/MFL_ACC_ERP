using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public interface ICurrencyService
    {
        List<ACC_CURRENCY> GetCurrencyList(string comp_code);
        bool DeleteCurrency(int id);
        bool SaveCurrency(int id, ACC_CURRENCY model);
        ACC_CURRENCY GetCurrencyById(int id);
    }
}
