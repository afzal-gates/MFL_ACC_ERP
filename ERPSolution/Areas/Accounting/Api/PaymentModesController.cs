using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/payment-modes")]
    public class PaymentModesController : BaseApiController
    {
        private readonly IPaymentModeService _paymentModeService;
        public PaymentModesController(IPaymentModeService paymentModeService)
        {
            _paymentModeService = paymentModeService;
        }

        [Route("get-payment-modes")]
        [HttpGet]
        public IActionResult GetPayementModes(string searchKey)
        {

            List<ACC_PAYMENT_MODE>paymentModes= _paymentModeService.GetPayementModes(searchKey);

            return Ok(new ResponseMessage<List<ACC_PAYMENT_MODE>>()
            {
                Result = paymentModes
            });

        }
        [Route("save-payment-mode")]
        [HttpPost]
        [ModelValidation]
        public IActionResult SavePaymentMode([FromBody]ACC_PAYMENT_MODE model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            return Ok(new ResponseMessage<bool>()
            {
                Result = _paymentModeService.SavePaymentMode(0, model)
            });

        }

        [Route("update-payment-mode")]
        [HttpPut]
        [ModelValidation]
        public IActionResult UpdateCompany(int id, [FromBody]ACC_PAYMENT_MODE model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            return Ok(new ResponseMessage<bool>()
            {
                Result = _paymentModeService.SavePaymentMode(id, model)
            });
        }

        [Route("get-payment-mode")]
        [HttpGet]
        public IActionResult GetPaymentMode(int id)
        {
            return Ok(new ResponseMessage<ACC_PAYMENT_MODE>()
            {
                Result = _paymentModeService.GetPaymentModeById(id)
            });

        }

        [Route("delete-payment-mode")]
        [HttpDelete]
        public IActionResult DeletePaymentMode(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _paymentModeService.DeletePaymentMode(id)
            });

        }

    }
}
