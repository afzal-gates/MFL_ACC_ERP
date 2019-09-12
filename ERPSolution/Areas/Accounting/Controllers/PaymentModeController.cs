using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPSolution.Controllers;

namespace ERPSolution.Areas.Accounting.Controllers
{
    public class PaymentModeController : BaseController
    {

        public ViewResult Index()
        {
            return View();
        }
        public PartialViewResult _Edit()
        {
            return PartialView();
        }
        public PartialViewResult _PaymentModes()
        {
            return PartialView();
        }
        
    }
}
