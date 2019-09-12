using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Web.Http;
using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERPSolution.Areas.Accounting.Api
{
      [RoutePrefix("api/accounting/unilayer-chart-of-accounts")]
    public class UnilayerChartOfAccountsController : BaseApiController
    {
       
         private readonly IUnilayerChartofAccountService _chartOfAccountService;
         public UnilayerChartOfAccountsController(IUnilayerChartofAccountService chartOfAccountService)
        {
            this._chartOfAccountService = chartOfAccountService;
        }
        [Route("get-chart-of-accounts")]
        [HttpGet]
        public IHttpActionResult GetChartOfAccounts()
        {

            return Ok(new ResponseMessage<IEnumerable<TreeView>>()
            {
                Result = _chartOfAccountService.GetChartOfAccount()
            });

        }
        [Route("get-control-chart-of-accounts")]
        [HttpGet]
        public IHttpActionResult GetThreeLayerChartOfAccounts()
        {

            return Ok(new ResponseMessage<IEnumerable<TreeView>>()
            {
                Result = _chartOfAccountService.GetControlChartOfAccounts()
            });

        }
        [Route("save-chart-of-accounts")]
        [HttpPost]
        public IHttpActionResult SaveChartOfAccounts([FromBody]TreeView model)
        {
            return Ok(new ResponseMessage<TreeView>()
            {
                Result = _chartOfAccountService.SaveChartOfAccount(model)
            });
        }

        [Route("update-chart-of-accounts")]
        [HttpPut]
        public IHttpActionResult UpdateChartOfAccounts(string code, string controlCode, TreeView model)
        {
            return Ok(new ResponseMessage<TreeView>()
            {
                Result = _chartOfAccountService.UpdateChartOfAccount(code, controlCode, model),
            });

        }

        [Route("get-account-heads")]
        [HttpGet]
        public IHttpActionResult GetAccountHeards(string searchKey)
        {
            return Ok(new ResponseMessage<List<ACC_SUB_CLASSModel>>()
            {
                Result = _chartOfAccountService.GetAccountHeards(CompanyCode.comp_code, searchKey),
            });
        }

        [Route("get-account-main-heads")]
        [HttpGet]
        public IHttpActionResult GetMainClassHeads()
        {
            return Ok(new ResponseMessage<TreeView>()
            {
                Result = _chartOfAccountService.GetMainClassHeads(CompanyCode.comp_code)
            });
        }
        [Route("get-cash-account-heads")]
        [HttpGet]
        public IHttpActionResult GetCashGlHeads()
        {
            const string cashMapCode = Heads.CashHeads;
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = _chartOfAccountService.GetCashBankHeads(CompanyCode.comp_code, cashMapCode)
            });
        }
        [Route("get-bank-account-heads")]
        [HttpGet]
        public IHttpActionResult GetBankGlHeads()
        {
             const string bankMapCode = Heads.BankHeads;
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = _chartOfAccountService.GetCashBankHeads(CompanyCode.comp_code, bankMapCode)
            });
        }

        [Route("check-gl-transaction")]
        [HttpGet]
        public IHttpActionResult CheckGlTransactionExist(string gl_code, string control_code)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _chartOfAccountService.CheckGlTransactionExist(CompanyCode.comp_code, gl_code, control_code)
            });
        }

        [Route("delete-gl-account")]
        [HttpDelete]
        public IHttpActionResult DeleteGlAccount(string gl_code, string control_code,int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _chartOfAccountService.DeleteGlAccount(CompanyCode.comp_code, gl_code, control_code,id)
            });
        }

      
        
    }
}

