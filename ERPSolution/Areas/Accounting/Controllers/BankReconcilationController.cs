using ERPSolution.Controllers;

using System.Web.Mvc;

namespace ERPSolution.Areas.Accounting.Controllers
{
    public class BankReconcilationController : BaseController
    {
    
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _ReconcileVouchers()
        {
            return PartialView();
        }
    }
}