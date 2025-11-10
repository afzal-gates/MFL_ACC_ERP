using ERP.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ERPSolution.Controllers
{

    [RoutePrefix("api/ext")]
    public class ExternalApiController : ControllerBase
    {
        [Route("GetOrderByBookingDate")]
        [HttpGet]
        [ExternalReqAuthorize]
        // GET :  /api/ext/GetOrderByBookingDate?pBOOKING_DT=date
        public IActionResult GetOrderByBookingDate(DateTime? pBOOKING_DT)
        {
            var obList = new BlkSmpModel().GetOrderByBookingDate(pBOOKING_DT);
            return Ok(obList);
        }




    }
}
