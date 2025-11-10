using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;
using ERPSolution.Areas.Accounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/voucher-masters")]
    public class VoucherMastersController : BaseApiController
    {
        private readonly ICurrencyService _currencyService;
        private readonly IVoucherMasterService _voucherMasterService;
        private readonly IVoucherTypeService _voucherTypeService;
        private readonly ICostCenterService _costCenterService;
        private readonly IPaymentModeService paymentModeService;

        public VoucherMastersController(ICurrencyService currencyService, IPaymentModeService paymentModeService, ICostCenterService costCenterService, IVoucherTypeService voucherTypeService, IVoucherMasterService voucherMasterService)
        {
            this._voucherMasterService = voucherMasterService;
            this._voucherTypeService = voucherTypeService;
            this._costCenterService = costCenterService;
            this.paymentModeService = paymentModeService;
            this._currencyService = currencyService;
        }

        [Route("get-voucher-masters")]
        [HttpGet]
        public IActionResult GetVoucherMasters(string searchRefNo, string searchVhcNo, DateTime? searchDate, int pn, int ps)
        {
            int total = 0;
            dynamic voucherList = _voucherMasterService.GetVoucherMasters(searchRefNo, searchVhcNo, searchDate, pn, ps, out total);

            return Ok(new ResponseMessage<dynamic>()
            {
                Result = voucherList,
                Total = total
            });
        }
        [Route("get-voucher-master")]
        [HttpGet]
        public IActionResult GetVoucherMaster(string id)
        {

            VoucherMasterViewModel model = new VoucherMasterViewModel
            {
                ACC_VOUCHER_MASTER = _voucherMasterService.GetVoucherMaster(CompanyCode.comp_code, id, Convert.ToInt16(base.UserId))
            };
            if (model.ACC_VOUCHER_MASTER.VOUCHER_MASTER_ID <= 0)
            {
                model.ACC_VOUCHER_MASTER.CHQ_DATE = DateTime.Now;
                model.ACC_VOUCHER_MASTER.POST_DATE = DateTime.Now;
            }
            model.ACC_VOUCHER_DETAIL.COST_CENTER_ID = 1;
            model.ACC_VOUCHER_DETAIL.CURRENCY_ID = 1;
            model.ACC_VOUCHER_DETAIL.EXCHANGE_RATE = 1;
            model.Currencies = _currencyService.GetCurrencyList(CompanyCode.comp_code);
   
            model.CostCenters = _costCenterService.GetCostCenterSelectModels(CompanyCode.comp_code);
            model.VoucherTypes = _voucherTypeService.GetVoucherTypeSelectModels(CompanyCode.comp_code);
         
            return Ok(new ResponseMessage<VoucherMasterViewModel>()
            {
                Result = model
            });
        }
        [Route("save-voucher-master")]
        [HttpPost]
        [ModelValidation]
        public IActionResult SaveVoucherMaster([FromBody]ACC_VOUCHER_MASTER model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            model.EMPLOYEE_ID = base.UserId;
            return Ok(new ResponseMessage<ACC_VOUCHER_MASTER>()
            {
                Result = _voucherMasterService.SaveVoucherMaster(0, model)
            });

        }

        [Route("update-voucher-master")]
        [HttpPut]
        [ModelValidation]
        public IActionResult UpdateVoucherMaster(int id, [FromBody]ACC_VOUCHER_MASTER model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            model.EMPLOYEE_ID = base.UserId;
            return Ok(new ResponseMessage<ACC_VOUCHER_MASTER>()
            {
                Result = _voucherMasterService.SaveVoucherMaster(id, model)
            });

        }


        [Route("delete-voucher-master")]
        [HttpDelete]
        public IActionResult DeleteVoucherMaster(int id)
        {
            string userId = base.UserId;
            return Ok(new ResponseMessage<bool>()
            {
                Result = _voucherMasterService.DeleteVoucherMaster(id, userId)
            });

        }
        [Route("save-tem-voucher-detail")]
        [HttpPost]
        [ModelValidation]
        public IActionResult SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail)
        {
            voucheDetail.COMP_CODE = CompanyCode.comp_code;
            voucheDetail.USER_ID = base.UserId;
            ACC_TEMP_VOUCHER_DETAIL vd = _voucherMasterService.SaveTemVoucheDetail(voucheDetail);
            return Ok(new ResponseMessage<List<ACC_VOUCHER_DETAIL>>()
            {
                Result = new List<ACC_VOUCHER_DETAIL>()
            });
        }
        [Route("get-voucher-temp-details")]
        [HttpGet]
        public IActionResult GetTempVoucheDetail()
        {
            var compCode = CompanyCode.comp_code;
            var userId = base.UserId;
            List<ACC_TEMP_VOUCHER_DETAIL> vtds = _voucherMasterService.GetTempVoucheDetail(userId, compCode);
            bool isEqualAmt = Math.Abs(Math.Round(vtds.Sum(x => x.DR_AMT), 2) - Math.Round(vtds.Sum(x => x.CR_AMT), 2)) == 0;
            double nAmt = Math.Round(vtds.Sum(x => x.DR_AMT) - vtds.Sum(x => x.CR_AMT), 3);
            double ndrAmt = 0.0;
            double ncrAmt = 0.0;
            if (nAmt > 0)
            {
                ncrAmt = nAmt;
            }
            else
            {
                ndrAmt = 0 - nAmt;
            }
            object temData = new
            {
                Vtemtdetails = vtds,
                IsEqualAmt = isEqualAmt,
                NdrAmt = ndrAmt,
                NcrAmt = ncrAmt

            };
            return Ok(new ResponseMessage<object>()
            {
                Result = temData
            });
        }
        [Route("delete-tem-voucher-detail")]
        [HttpDelete]
        public IActionResult DeleteTemVoucheDetail(int id)
        {
            var userId = base.UserId;
            bool isDeleted = _voucherMasterService.DeleteTemVoucheDetail(id, userId);
            return Ok(new ResponseMessage<bool>()
            {
                Result = isDeleted
            });
        }

        [Route("get-last-narration")]
        [HttpGet]
        public IActionResult GetLastNarration(string accountCode)
        {
            var userId = base.UserId;
            return Ok(new ResponseMessage<string>()
            {
                Result = _voucherMasterService.GetLastNarration(userId, CompanyCode.comp_code, accountCode)
            });
        }

        [Route("get-bank-reconcile-vouchers")]
        [HttpGet]
        public IActionResult GetBankReconsileVouchers(DateTime? fromDate,DateTime? toDate,string accountHead,int status)
        {
            var userId = base.UserId;
            return Ok(new ResponseMessage<object>()
            {
                Result = _voucherMasterService.GetBankReconsileVouchers(userId, CompanyCode.comp_code, fromDate,toDate,accountHead, status)
            });
        }

        [Route("update-bank-date")]
        [HttpPut]
        public IActionResult UpdateBankDate(int id,  DateTime? bankDate)
        {
            
                return Ok(new ResponseMessage<bool>()
                {
                    Result = _voucherMasterService.UpdateBankDate(CompanyCode.comp_code, id, bankDate)
                });

        }

        [Route("get-pending-bills")]
        [HttpGet]
        public IActionResult GetPandingBills(string ac_code,string main_code,string sub_code,DateTime?fromDate,DateTime?toDate)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = _voucherMasterService.GetPandingBills(CompanyCode.comp_code, ac_code, main_code, sub_code,fromDate,toDate)
            });
        }

        [Route("get-payment-modes")]
        [HttpGet]
        public IActionResult GetPaymentModes(int voucherTypeId)
        {
            return Ok(new ResponseMessage<object>()
            {
                Result = _voucherMasterService.GetPaymentModes(CompanyCode.comp_code, voucherTypeId)
            });
        }

        [Route("get-lat-narration")]
        [HttpGet]
        public IActionResult GetLastNarrations()
        {
            return Ok(new ResponseMessage<string>()
            {
                Result = _voucherMasterService.GetLastNarration(CompanyCode.comp_code, base.UserId)
            });
        }
    }
}
