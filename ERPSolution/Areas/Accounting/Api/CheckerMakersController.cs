using ERP.BLL;
using ERP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ERPSolution.Areas.Accounting.Api
{
    [System.Web.Http.RoutePrefix("api/accounting/checker-makers")]
    public class CheckerMakersController :ApiController
    {
        public readonly ICheckerMakerService checkerMakerService;
        public CheckerMakersController(ICheckerMakerService checkerMakerService)
        {
            this.checkerMakerService = checkerMakerService;
        }

        [Route("update-vocher-master-post-id")]
        [HttpPost]
        public IHttpActionResult UpdateCheckerMaker()
        {
            return Ok(new ResponseMessage<int>()
            {
                Result = checkerMakerService.UpdateCheckerMaker()
            });

        }

        [Route("update-bill-reconciliation")]
        [HttpPost]
        public IHttpActionResult UpdateBillReconciliation()
        {
            return Ok(new ResponseMessage<int>()
            {
                Result = checkerMakerService.UpdateBillReconciliation()
            });

        }
   
    }
}