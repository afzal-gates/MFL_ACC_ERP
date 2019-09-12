
using System.Web.Mvc;
using ERPSolution.Controllers;

namespace ERPSolution.Areas.Accounting.Controllers
{
    public class UnilayerChartOfAccountController : BaseController
    {
        public ViewResult Index()
        {
            return View();
        }
        public PartialViewResult _TreeView()
        {
            return PartialView();
        }
        public PartialViewResult _Create()
        {
            return PartialView();
        }
	}
}