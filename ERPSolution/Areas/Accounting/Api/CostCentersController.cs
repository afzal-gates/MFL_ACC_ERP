using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using ERP.Shared;
using ERPSolution.Areas.Accounting.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ERPSolution.Areas.Accounting.Api
{
   [RoutePrefix("api/accounting/cost-centers")]
    public class CostCentersController : ControllerBase
    {
        private readonly ICostCenterService costCenterService;
        private readonly ICostCenterGroupService costCenterGroupService;
        public CostCentersController(ICostCenterService costCenterService, ICostCenterGroupService costCenterGroupService)
        {
            this.costCenterService = costCenterService;
            this.costCenterGroupService = costCenterGroupService;
        }

        [Route("get-cost-centers")]
        [HttpGet]
        public IActionResult GetCostCenters(string searchText,  int pn, int ps)
        {
            int total = 0;
            return Ok(new ResponseMessage<List<ACC_COST_CENTER>>()
            {
                Result = costCenterService.GetCostCenters(searchText, pn, ps, out total),Total=total

            });

        }
        [Route("save-cost-center")]
        [HttpPost]
        public IActionResult SaveCostCenter([FromBody]ACC_COST_CENTER model)
        {
            return Ok(new ResponseMessage<ACC_COST_CENTER>()
            {
                Result = costCenterService.SaveCostCenter(0,model)
            });

        }
        [Route("update-cost-center")]
        [HttpPut]
        public IActionResult UpdateCostCenter(int id,[FromBody]ACC_COST_CENTER model)
        {
            return Ok(new ResponseMessage<ACC_COST_CENTER>()
            {
                Result = costCenterService.SaveCostCenter(id,model)
            });
        }

        [Route("get-cost-center")]
        [HttpGet]
        public IActionResult GetCostCenter(int id)
        {
            CostCenterViewModel model = new CostCenterViewModel
            {
                CostCenter = costCenterService.GetCostCenterById(id),
                Groups = costCenterGroupService.GetCostCenterGroups()
            };
            return Ok(new ResponseMessage<CostCenterViewModel>()
            {
                Result = model
            });

        }

        [Route("delete-cost-center")]
        [HttpDelete]
        public IActionResult DeleteCostCenter(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = costCenterService.DeleteCostCenter(id)
            });

        }

        [Route("get-cost-center-select-models")]
        [HttpGet]
        public IActionResult CostCenterSelectModels()
        {
            return Ok(new ResponseMessage<List<SelectModel>>()
            {
                Result = costCenterService.GetCostCenterSelectModels(CompanyCode.comp_code)
            });

        }
    }
}
