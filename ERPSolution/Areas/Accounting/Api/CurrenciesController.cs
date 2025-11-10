using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using ERP.Shared;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/currencies")]
    public class CurrenciesController: BaseApiController
    {
        private readonly ICurrencyService _currencyService;
        public CurrenciesController(ICurrencyService currencyService)
        {
            this._currencyService = currencyService;
        }
        [Route("get-currencies")]
        [HttpGet]
        public IActionResult GetCurrencies()
        {
            return Ok(new ResponseMessage<List<ACC_CURRENCY>>()
            {
                Result = _currencyService.GetCurrencyList(CompanyCode.comp_code)
            });
        }

        [Route("save-currency")]
        [HttpPost]
        [ModelValidation]
        public IActionResult SaveCurrency([FromBody]ACC_CURRENCY model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            return Ok(new ResponseMessage<bool>()
            {
                Result = _currencyService.SaveCurrency(0, model)
            });

        }

        [Route("update-currency")]
        [HttpPut]
        [ModelValidation]
        public IActionResult UpdateCurrency(int id, [FromBody]ACC_CURRENCY model)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _currencyService.SaveCurrency(id, model)
            });
        }


        [Route("get-currency")]
        [HttpGet]
        public IActionResult GetCurrency(int id)
        {
            return Ok(new ResponseMessage<ACC_CURRENCY>()
            {
                Result = _currencyService.GetCurrencyById(id)
            });
        }

        [Route("delete-currency")]
        [HttpDelete]
        public IActionResult DeleteCurrency(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _currencyService.DeleteCurrency(id)
            });

        }
    }
}
