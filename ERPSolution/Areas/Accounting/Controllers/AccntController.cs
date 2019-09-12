using ERPSolution.Controllers;
using System.Web.Mvc;

namespace ERPSolution.Areas.Accounting.Controllers
{    
    public class AccntController : BaseController
    {
   
        public ViewResult VchType()
        {
            return View();
        }
        public PartialViewResult _VchType()
        {
            return PartialView();
        }

        public PartialViewResult _VchTypeList()
        {
            return PartialView();
        }
        

    }
}