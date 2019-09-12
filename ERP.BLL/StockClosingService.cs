using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public class StockClosingService : IStockClosingService
    {
        private readonly IStockClosingRepository stockClosingRepository;
        private readonly IVoucherMasterRepository voucherMasterRepository;
        public StockClosingService(IStockClosingRepository stockClosingRepository, IVoucherMasterRepository voucherMasterRepository)
        {
            this.stockClosingRepository = stockClosingRepository;
            this.voucherMasterRepository = voucherMasterRepository;
        }


        public List<ACC_STOCK_CLOSING> GetMonthlyStockClosing(int month_code, int year_code)
        {
            List<ACC_STOCK_CLOSING> stocks = new List<ACC_STOCK_CLOSING>();
            if (month_code > 0 && year_code > 0)
            {
                stocks = stockClosingRepository.GetMonthlyStockClosing(month_code, year_code);
            }
            return stocks;
        }

        public int SaveMonthlyStockClosing(ACC_STOCK_CLOSING model)
        {
            if (model.CLOSING > 0 && model.YEAR_CODE > 0 && model.MONTH_CODE > 0)
            {

                return stockClosingRepository.SaveMonthlyStockClosing(model);
            }
            throw new MultiTexInvalidDataException("Please Enter valid Cloading value");


        }

        public int UpdateMonthlyStockClosing(int year_code, int month_code, string userId, string comp_code)
        {
            int lastDateOfTheMonth = DateTime.DaysInMonth(year_code, month_code);
            var postId = voucherMasterRepository.GetNewPostID(comp_code);
            ACC_VOUCHER_MASTER vm = new ACC_VOUCHER_MASTER();
            vm.POST_ID = postId;
            vm.POST_DATE = new DateTime(year_code, month_code, lastDateOfTheMonth);
            vm.VOUCHER_NO = "STC-" + year_code + month_code.ToString().PadLeft(2, '0');
            vm.EMPLOYEE_ID = userId;
            vm.VOUCHER_TYPE_ID = 5;
            vm.DESCRIPTION = String.Format("Closing Stock for the month of {0}-{1}", vm.POST_DATE.ToString("MMMM"), year_code);
            vm.REF_ID = vm.VOUCHER_NO;
            vm.DIN = "20";
            vm.COMP_CODE = comp_code;
            vm.CREATED_DATE = DateTime.Now;
            vm.CREATED_BY = userId;
            vm.LAST_UPDATED_BY = Convert.ToInt64(userId);
            vm.LAST_UPDATE_DATE = DateTime.Now;

            int saved = stockClosingRepository.UpdateMonthlyStockClosing(vm, year_code, month_code);
            if (saved > 0)
            {
                saved += UpdateMonthlyStockOpening(year_code, month_code, userId, comp_code);
            }
            return saved;


        }

        public int UpdateMonthlyStockOpening(int year_code, int month_code, string userId, string comp_code)
        {
            int firstDateOfTheMonth = DateTime.DaysInMonth(year_code, month_code);
            var postId = voucherMasterRepository.GetNewPostID(comp_code);
            ACC_VOUCHER_MASTER vm = new ACC_VOUCHER_MASTER();
            vm.POST_ID = postId;
            vm.POST_DATE = new DateTime(year_code, month_code, firstDateOfTheMonth);
            vm.POST_DATE = vm.POST_DATE.Date.AddDays(1);
            vm.VOUCHER_NO = "STO-" + year_code + month_code.ToString().PadLeft(2, '0');
            vm.EMPLOYEE_ID = userId;
            vm.VOUCHER_TYPE_ID = 5;
            vm.DESCRIPTION = String.Format("Opening Stock for the month of {0}-{1}", vm.POST_DATE.ToString("MMMM"), year_code);
            vm.REF_ID = vm.VOUCHER_NO;
            vm.DIN = "21";
            vm.COMP_CODE = comp_code;
            vm.CREATED_DATE = DateTime.Now;
            vm.CREATED_BY = userId;
            vm.LAST_UPDATED_BY = Convert.ToInt64(userId);
            vm.LAST_UPDATE_DATE = DateTime.Now;

            return stockClosingRepository.UpdateMonthlyStockOpening(vm, year_code, month_code);

        }
    }
}
