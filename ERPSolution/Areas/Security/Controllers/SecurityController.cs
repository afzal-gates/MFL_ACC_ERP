using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Security.Controllers
{
    public class SecurityController : BaseController
    {
        //
        // GET: /Security/Security/

        public ActionResult RequestApprovalWorkFlow()
        {
            return View();
        }
        public PartialViewResult _RequestApprovalWorkFlow()
        {
            return PartialView();
        }

        public ActionResult UserStoreMap()
        {
            return View();
        }
        public PartialViewResult _UserStoreMap()
        {
            return PartialView();
        }

        public ActionResult UserOfficeMap()
        {
            return View();
        }
        public PartialViewResult _UserOfficeMap()
        {
            return PartialView();
        }


        public ActionResult ReportTemplate()
        {
            return View();
        }
        public PartialViewResult _ReportTemplate()
        {
            return PartialView();
        }

        public ViewResult GlobalNotification()
        {
            return View();
        }

        public PartialViewResult _GlobalNotification()
        {
            return PartialView();
        }

        public ViewResult serverMaintenance()
        {

            return View();
        }
        public PartialViewResult _serverMaintenance()
        {

            return PartialView();
        }
	}
}