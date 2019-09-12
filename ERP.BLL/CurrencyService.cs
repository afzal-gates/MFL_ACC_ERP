using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Data;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }
        public List<ACC_CURRENCY> GetCurrencyList(string comp_code)
        {
            return currencyRepository.GetCurrencyList(comp_code);
        }

        public bool DeleteCurrency(int id)
        {
            return currencyRepository.DeleteCurrency(id);
        }

        public bool SaveCurrency(int id, ACC_CURRENCY model)
        {

            return currencyRepository.SaveCurrency(id, model);
        }

        public ACC_CURRENCY GetCurrencyById(int id)
        {
            return currencyRepository.GetCurrencyById(id);
        }
    }
}
