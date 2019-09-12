using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface ICurrencyRepository
    {
        List<ACC_CURRENCY> GetCurrencyList(string comp_code);
        ACC_CURRENCY GetCurrencyById(int id);
        bool SaveCurrency(int id, ACC_CURRENCY model);
        bool DeleteCurrency(int id);
    }
}
