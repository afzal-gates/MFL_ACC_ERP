using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;
using System.Collections.Generic;
using System.Web.Http;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/cost-center-groups")]
    public class CostCenterGroupsController : ApiController
    {
        private readonly ICostCenterGroupService costCenterGroupService;
        public CostCenterGroupsController(ICostCenterGroupService costCenterGroupService)
        {
            this.costCenterGroupService = costCenterGroupService;
        }

        [Route("get-cost-center-groups")]
        [HttpGet]
        public IHttpActionResult GetCostCenterGroups()
        {
            return Ok(new ResponseMessage<List<ACC_COST_CENTER_GROUP>>()
            {
                Result = costCenterGroupService.GetCostCenterGroups()
            });

        }
        [Route("save-cost-center-group")]
        [HttpPost]
        public IHttpActionResult SaveCostCenterGroup([FromBody]ACC_COST_CENTER_GROUP model)
        {
            model.COMP_CODE = CompanyCode.comp_code;
            return Ok(new ResponseMessage<ACC_COST_CENTER_GROUP>()
            {
                Result = costCenterGroupService.SaveCostCenterGroup(0, model)
            });

        }
        [Route("update-cost-center-group")]
        [HttpPut]
        public IHttpActionResult UpdateCostCenterGroup(int id, [FromBody]ACC_COST_CENTER_GROUP model)
        {
            model.COMP_CODE = CompanyCode.comp_code;


            return Ok(new ResponseMessage<ACC_COST_CENTER_GROUP>()
            {
                Result = costCenterGroupService.SaveCostCenterGroup(id, model)
            });
        }

        [Route("get-cost-center-group")]
        [HttpGet]
        public IHttpActionResult GetCostCenterGroup(int id)
        {
            return Ok(new ResponseMessage<ACC_COST_CENTER_GROUP>()
            {
                Result = costCenterGroupService.GetCostCenterGroupById(id)
            });

        }

        [Route("delete-cost-center-group")]
        [HttpDelete]
        public IHttpActionResult DeleteCostCenterGroup(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = costCenterGroupService.DeleteCostCenterGroup(id)
            });

        }
    }
}
