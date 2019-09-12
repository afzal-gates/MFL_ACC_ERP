
using System.Collections.Generic;
using System.Web.Http;
using ERP.BLL;
using ERP.Model;
using ERP.Core;
using ERP.Model.Accounting;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/voucher-types")]
    public class VoucherTypesController : BaseApiController
    {
      
       private readonly IVoucherTypeService _voucherTypeService;
        public VoucherTypesController(IVoucherTypeService voucherTypeService)
        {
             this._voucherTypeService = voucherTypeService;
        }

        [Route("get-voucher-types")]
        [HttpGet]
        public IHttpActionResult SelectVoucherTypes()
        {
            return Ok(new ResponseMessage<List<ACC_VOUCHER_TYPEModel>>()
            {
                Result = _voucherTypeService.GetVoucherTypes()
            });

        }
        [Route("get-voucher-type")]
        [HttpGet]
        public IHttpActionResult GetVoucherType(int voucherTypeId)
        {
            return Ok(new ResponseMessage<ACC_VOUCHER_TYPEModel>()
            {
                Result =_voucherTypeService.GetVoucerType(voucherTypeId)
            });
        }

        [Route("save-voucher-type")]
        [HttpPost]
        [ModelValidation]
        public IHttpActionResult SaveVoucherType(ACC_VOUCHER_TYPEModel ob)
        {
            
            return Ok(new ResponseMessage<string>()
            {
                Result = _voucherTypeService.SaveVoucherType(ob)
            });
        }

        [Route("delete-voucher-type")]
        [HttpDelete]
        public IHttpActionResult DeleteVoucherType(int id)
        {

            return Ok(new ResponseMessage<bool>()
            {
                Result = _voucherTypeService.DeleteVoucherType(id)
            });
        }
    }
}
