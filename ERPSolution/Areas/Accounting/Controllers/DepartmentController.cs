using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Accounting.Controllers
{
    public class DepartmentController : BaseController
    {
       
        public ViewResult Index()
        {
            return View();
        }
        public PartialViewResult _CostCenterList()
        {
            return PartialView();
        }
        public PartialViewResult _Edit()
        {
            return PartialView();
        }
    }
}