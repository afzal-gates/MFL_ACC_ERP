using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;


namespace ERPSolution.Areas.Hr.Controllers
{    
    public class HrCompanyController : BaseController
    {
        
        // GET: Test/Country
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public string Update(HrCompanyModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
                vMsg = ob.Update();

            return vMsg;
        }

        [HttpPost]
        public string Save(HrCompanyModel ob)
        {
            string vMsg = "";
            
            if(ModelState.IsValid)
                vMsg = ob.Save();
            
            return vMsg;            
        }

        

        public JsonResult LookupListData()
        {            
            Int64 lookupTableId = Convert.ToInt64(HttpContext.Request.Params["pLookupTableId"]);

            var ob = new LookupDataModel().LookupListData(lookupTableId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompanyListData()
        {
            var ob = new HrCompanyModel().CompanyListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
    }
}