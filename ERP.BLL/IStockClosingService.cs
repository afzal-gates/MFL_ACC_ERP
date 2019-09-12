using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public interface IStockClosingService
    {
        List<ACC_STOCK_CLOSING> GetMonthlyStockClosing(int month_code, int year_code);
        int SaveMonthlyStockClosing(ACC_STOCK_CLOSING model);
        int UpdateMonthlyStockClosing(int year_code, int month_code, string userId, string comp_code);
    }
}
