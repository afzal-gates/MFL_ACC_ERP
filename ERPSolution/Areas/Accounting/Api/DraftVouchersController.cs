using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;
using ERPSolution.Areas.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/draft-vouchers")]
    public class DraftVouchersController : BaseApiController
    {
        private readonly ICurrencyService _currencyService;
        private readonly IDraftVoucherService _voucherMasterService;
        private readonly IVoucherTypeService _voucherTypeService;
        private readonly ICostCenterService _costCenterService;
        private readonly IPaymentModeService _paymentModeService;

        public DraftVouchersController(ICurrencyService currencyService, IPaymentModeService paymentModeService, ICostCenterService costCenterService, IVoucherTypeService voucherTypeService, IDraftVoucherService voucherMasterService)
        {
            this._voucherMasterService = voucherMasterService;
            this._voucherTypeService = voucherTypeService;
            this._costCenterService = costCenterService;
            this._paymentModeService = paymentModeService;
            this._currencyService = currencyService;
        }

        [Route("get-draft-vouchers")]
        [HttpGet]

        public IHttpActionResult GetVoucherQueues(string searchRefNo, string searchVhcNo, DateTime? searchDate, int pn, int ps)
        {
            int total = 0;
            dynamic voucherList = _voucherMasterService.GetVoucherMasters(searchRefNo, searchVhcNo, searchDate, pn, ps, out total);
            return Ok(new ResponseMessage<dynamic>()
            {
                Result = voucherList,
                Total = total
            });
        }
        [Route("get-draft-voucher")]
        [HttpGet]
        public IHttpActionResult GetVoucherQueue(string id)
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
            model.PaymentModes = _paymentModeService.GetPaymentModes(CompanyCode.comp_code);
            return Ok(new ResponseMessage<VoucherMasterViewModel>()
            {
                Result = model
            });
        }
        [Route("save-draft-voucher")]
        [HttpPost]
        [ModelValidation]
        public IHttpActionResult SaveVoucherQueue([FromBody]ACC_VOUCHER_MASTER model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            model.EMPLOYEE_ID = base.UserId;
            return Ok(new ResponseMessage<ACC_VOUCHER_MASTER>()
            {
                Result = _voucherMasterService.SaveVoucherMaster(0, model)
            });
        }

        [Route("update-draft-voucher")]
        [HttpPut]
        [ModelValidation]
        public IHttpActionResult UpdateVoucherQueue(int id, [FromBody]ACC_VOUCHER_MASTER model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            model.EMPLOYEE_ID = base.UserId;
            return Ok(new ResponseMessage<ACC_VOUCHER_MASTER>()
            {
                Result = _voucherMasterService.SaveVoucherMaster(id, model)
            });
        }


        [Route("delete-draft-voucher")]
        [HttpDelete]
        public IHttpActionResult DeleteVoucherMaster(int id)
        {
            string userId = base.UserId;
            return Ok(new ResponseMessage<bool>()
            {
                Result = _voucherMasterService.DeleteVoucherMaster(id, userId)
            });

        }
        [Route("save-tem-draft-voucher")]
        [HttpPost]
        [ModelValidation]
        public IHttpActionResult SaveTemVoucheDetail(ACC_TEMP_VOUCHER_DETAIL voucheDetail)
        {
            voucheDetail.COMP_CODE = CompanyCode.comp_code;
            voucheDetail.USER_ID = base.UserId;
            ACC_TEMP_VOUCHER_DETAIL vd = _voucherMasterService.SaveTemVoucheDetail(voucheDetail);
            return Ok(new ResponseMessage<List<ACC_VOUCHER_DETAIL>>()
            {
                Result = new List<ACC_VOUCHER_DETAIL>()
            });
        }
        [Route("get-temp-draft-vouchers")]
        [HttpGet]
        public IHttpActionResult GetTempVoucheDetail()
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
        [Route("delete-temp-draft-voucher")]
        [HttpDelete]
        public IHttpActionResult DeleteTemVoucheDetail(int id)
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
        public IHttpActionResult GetLastNarration(string accountCode)
        {
            var userId = base.UserId;
            return Ok(new ResponseMessage<string>()
            {
                Result = _voucherMasterService.GetLastNarration(userId, CompanyCode.comp_code, accountCode)
            });
        }
    }
}
