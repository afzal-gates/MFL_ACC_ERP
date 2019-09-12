using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Controllers
{
    public class GlobalUIController : Controller
    {
        //
        // GET: /GlobalUI/
        public ActionResult BrandEntry()
        {
            return View("BrandEntry");
        }

        public ActionResult BankEntry()
        {
            return View("BankEntry");
        }

        public ActionResult BankBranchEntry()
        {
            return View("BankBranchEntry");
        }
        
	}
}