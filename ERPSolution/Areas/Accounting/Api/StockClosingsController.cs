using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERPSolution.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ERP.Shared;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/stock-closings")]
    public class StockClosingsController : BaseApiController
    {

        private readonly IStockClosingService stockClosingService;
        public StockClosingsController(IStockClosingService stockClosingService)
        {
            this.stockClosingService = stockClosingService;
        }

        [Route("get-stock-closings")]
        [HttpGet]
        public IActionResult GetMonthlyStockClosing(int month_code,int year_code)
        {
            StockCloasingViewModel model = new StockCloasingViewModel();
            model.Stocks = stockClosingService.GetMonthlyStockClosing(month_code, year_code);
            model.Years = new List<int>(Enumerable.Range(DateTime.Now.Year-1, DateTime.Now.Year - DateTime.Now.Year + 3));
            model.Months = new List<SelectModel>();
            for (int i = 1; i <= 12; i++)
            {
                model.Months.Add(new SelectModel() { Value = i, Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) });
            }
            return Ok(new ResponseMessage<StockCloasingViewModel>()
            {
                Result = model
            });
        }

        [Route("save-stock-closing")]
        [HttpPost]
        [ModelValidation]
        public IActionResult SaveMonthlyStockClosing([FromBody]ACC_STOCK_CLOSING model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            int id= stockClosingService.SaveMonthlyStockClosing(model);
            return Ok(new ResponseMessage<int>()
            {
                Result = id
            });
        }

        [Route("save-monthly-stock-ledger")]
        [HttpPost]
        [ModelValidation]
        public IActionResult SaveMonthlyStockClosingToAccounts(int year_code,int month_code)
        {

            int id = stockClosingService.UpdateMonthlyStockClosing(year_code, month_code,base.UserId,CompanyCode.comp_code);
            return Ok(new ResponseMessage<int>()
            {
                Result = id
            });
        }
    }
}
