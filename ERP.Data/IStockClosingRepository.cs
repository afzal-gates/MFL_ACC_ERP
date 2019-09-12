using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface IStockClosingRepository
    {
        List<ACC_STOCK_CLOSING> GetMonthlyStockClosing(int month_code, int year_code);
        int SaveMonthlyStockClosing(ACC_STOCK_CLOSING model);
        int UpdateMonthlyStockClosing(ACC_VOUCHER_MASTER vm, int year_code, int month_code);
        int UpdateMonthlyStockOpening(ACC_VOUCHER_MASTER vm, int year_code, int month_code);
        
    }
}
