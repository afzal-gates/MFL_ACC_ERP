using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using System.Threading;
using Hangfire;
using RazorEngine;
using System.IO;
using System.Web.Hosting;
using Postal;
using Microsoft.AspNet.SignalR;
using System.Configuration;
using ERPSolution.Common;

namespace ERPSolution.Controllers
{
    public class HomeController : BaseController
    {
        [SignInCheck]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _ChangePasswordModal()
        {
            return View();
        }

        public PartialViewResult UserDashBoard()
        {
            return PartialView();
        }



    }
}