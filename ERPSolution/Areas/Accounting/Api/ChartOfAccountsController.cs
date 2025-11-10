using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/chart-of-accounts")]
    public class ChartOfAccountsController : ControllerBase
    {
        private readonly IChartOfAccountService _chartOfAccountService;
        public ChartOfAccountsController(IChartOfAccountService chartOfAccountService)
        {
            this._chartOfAccountService = chartOfAccountService;
        }
        [Route("get-chart-of-accounts")]
        [HttpGet]
        public IActionResult GetChartOfAccounts()
        {
            return Ok(new ResponseMessage<IEnumerable<TreeView>>()
            {
                Result = _chartOfAccountService.GetChartOfAccount()
            });

        }
        [Route("save-chart-of-accounts")]
        [HttpPost]
        public IActionResult SaveChartOfAccounts([FromBody]TreeView model)
        {
            return Ok(new ResponseMessage<TreeView>()
            {
                Result = _chartOfAccountService.SaveChartOfAccount(model)
            });
        }

        [Route("update-chart-of-accounts")]
        [HttpPut]
        public IActionResult UpdateChartOfAccounts(string code, string controlCode, TreeView model)
        {
            return Ok(new ResponseMessage<TreeView>()
            {
                Result = _chartOfAccountService.UpdateChartOfAccount(code, controlCode, model),
            });

        }

        [Route("get-account-heads")]
        [HttpGet]
        public IActionResult GetAccountHeards(string searchKey)
        {
            return Ok(new ResponseMessage<dynamic>()
            {
                Result = _chartOfAccountService.GetAccountHeards(CompanyCode.comp_code, searchKey),
            });
        }

        [Route("get-account-main-heads")]
        [HttpGet]
        public IActionResult GetMainClassHeads()
        {
            return Ok(new ResponseMessage<TreeView>()
            {
                Result = _chartOfAccountService.GetMainClassHeads(CompanyCode.comp_code)
            });
        }

    }
}
