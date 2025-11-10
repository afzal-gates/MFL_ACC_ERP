using ERP.BLL;
using ERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/checker-makers")]
    public class CheckerMakersController : ControllerBase
    {
        public readonly ICheckerMakerService checkerMakerService;
        public CheckerMakersController(ICheckerMakerService checkerMakerService)
        {
            this.checkerMakerService = checkerMakerService;
        }

        [Route("update-vocher-master-post-id")]
        [HttpPost]
        public IActionResult UpdateCheckerMaker()
        {
            return Ok(new ResponseMessage<int>()
            {
                Result = checkerMakerService.UpdateCheckerMaker()
            });

        }

        [Route("update-bill-reconciliation")]
        [HttpPost]
        public IActionResult UpdateBillReconciliation()
        {
            return Ok(new ResponseMessage<int>()
            {
                Result = checkerMakerService.UpdateBillReconciliation()
            });

        }
   
    }
}