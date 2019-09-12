using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using System.Collections;

namespace ERPSolution.Areas.Hr.Controllers
{    
    public class HrController : BaseController
    {        
        
        public ActionResult IncrMemo()
        {            
            return View();
        }

        public PartialViewResult _IncrMemoCreation()
        {
            return PartialView();
        }

        public ActionResult IncrProposal()
        {
            return View();
        }

        public PartialViewResult _IncrProposalH()
        {
            return PartialView();
        }
        public PartialViewResult _IncrProposalD()
        {
            return PartialView();
        }
              


        public PartialViewResult _IncrProposalBatchList()
        {
            return PartialView();
        }
        
        public ActionResult IncrHistory4Emp()
        {
            return View();
        }

        public PartialViewResult _IncrHistory4Emp()
        {
            return PartialView();
        }

    }
}