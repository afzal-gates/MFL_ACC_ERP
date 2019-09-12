using ERP.Model;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Controllers
{

    [RoutePrefix("api/common")]
    public class CommonApiController : ApiController
    {
        

        //[Route("CompanyList")]
        //[HttpGet]
        //// GET :  /api/common/CompanyList
        //public IHttpActionResult CompanyList()
        //{
        //    var obList = new HrCompanyModel().SelectAll();
        //    return Ok(obList);
        //}


        //[Route("OfficeList")]
        //[HttpGet]
        //// GET :  /api/common/OfficeList
        //public IHttpActionResult OfficeList()
        //{
        //    var obList = new HrOfficeModel().OfficeListData();
        //    return Ok(obList);
        //}

        //[Route("GetOfficeList")]
        //[HttpGet]
        //// GET :  /api/common/GetOfficeList
        //public IHttpActionResult GetOfficeList(Int32? pHR_COMPANY_ID = null)
        //{
        //    var obList = new HrOfficeModel().GetOfficeList(pHR_COMPANY_ID);
        //    return Ok(obList);
        //}


        //[Route("LocationList")]
        //[HttpGet]
        //// GET :  /api/common/LocationList
        //public IHttpActionResult LocationList()
        //{
        //    var obList = new RF_LOCATIONModel().SelectAll();
        //    return Ok(obList);
        //}

        //[Route("LookupListData/{ID:int}")]
        //[HttpGet]
        //// GET :  mrc/api/common/LookupListData
        //public IHttpActionResult LookupListData(Int64 ID)
        //{
        //    var obList = new LookupDataModel().LookupListData(ID);
        //    return Ok(obList);
        //}

        //[Route("UserData")]
        //[HttpGet]
        //// GET :  mrc/api/common/UserData
        //public IHttpActionResult UserData()
        //{
        //    var obList = new ScUserModel().SelectAll();
        //    return Ok(obList);
        //}

        //[Route("SelectAllUserData")]
        //[HttpGet]
        //// GET :  mrc/api/common/SelectAllUserData
        //public IHttpActionResult SelectAllUserData()
        //{
        //    var obList = new ScUserModel().SelectAllUserData();
        //    return Ok(obList);
        //}

        //[Route("getUserData/TnaTask/{ID}")]
        //[HttpGet]
        //// GET :  api/common/getUserData/TnaTask/1
        //public IHttpActionResult getUserData(Int64 ID)
        //{
        //    var obList = new ScUserModel().getUserData(ID);
        //    return Ok(obList);
        //}


        //[Route("SelectAllSampleTypeData")]
        //[HttpGet]
        //// GET :  mrc/api/common/SelectAllSampleTypeData
        //public IHttpActionResult SelectAllSampleTypeData()
        //{
        //    var obList = new RF_SMPL_TYPEModel().SelectAll();
        //    return Ok(obList);
        //}



        //[Route("MOUList/{Default:alpha?}")]
        //[HttpGet]
        //// GET :  api/common/MOUList/Y
        //public IHttpActionResult MOUList(String Default = "N")
        //{
        //    var obList = new RF_MOUModel().SelectAll(Default);
        //    return Ok(obList);
        //}

        //[Route("CurrencyList")]
        //[HttpGet]
        //// GET :  api/Common/CurrencyList
        //public IHttpActionResult CurrencyList()
        //{
        //    var obList = new RF_CURRENCYModel().SelectAll();
        //    return Ok(obList);
        //}

        //[Route("GetCountryList")]
        //[HttpGet]
        //public IHttpActionResult GetCountryList()
        //{
        //    var ob = new HR_COUNTRYModel().SelectAll();
        //    return Ok(ob);
        //}

        //[Route("BrandSave")]
        //[HttpPost]
        //// GET :  /api/common/BrandSave
        //public IHttpActionResult BrandSave([FromBody] RF_BRANDModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}


        //[Route("GetItemBrandList")]
        //[HttpGet]
        //public IHttpActionResult GetItemBrandList()
        //{
        //    var ob = new RF_BRANDModel().SelectAll();
        //    return Ok(ob);
        //}

        //[Route("GetCategoryWiseBrandList/{pINV_ITEM_CORE_CAT_ID:int}")]
        //[HttpGet]
        //public IHttpActionResult GetCategoryWiseBrandList(int pINV_ITEM_CORE_CAT_ID, Int16? pOption = 3002, String pKNT_YRN_LOT_ID_LST = null, string pIS_SOLID = "S")
        //{
        //    var ob = new RF_BRANDModel().CategoryWiseBrandList(pINV_ITEM_CORE_CAT_ID, pOption, pKNT_YRN_LOT_ID_LST, pIS_SOLID);
        //    return Ok(ob);
        //}

        //[Route("BankSave")]
        //[HttpPost]
        //// GET :  /api/common/BankSave
        //public IHttpActionResult BankSave([FromBody] RF_BANKModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

        //[Route("BankDataList")]
        //[HttpGet]
        //public IHttpActionResult BankDataList()
        //{
        //    var obList = new RF_BANKModel().SelectAll();
        //    return Ok(obList);
        //}

        //[Route("GetBankBranchList")]
        //[HttpGet]
        //public IHttpActionResult GetBankBranchList()
        //{
        //    var obList = new RF_BANK_BRANCHModel().SelectAll();
        //    return Ok(obList);
        //}

        //[Route("BankBranchDataList/{pRF_BANK_ID}")]
        //[HttpGet]
        //public IHttpActionResult BankBranchDataList(int? pRF_BANK_ID)
        //{
        //    var obList = new RF_BANK_BRANCHModel().BankBranchDataList(pRF_BANK_ID);
        //    return Ok(obList);
        //}

        //[Route("BankBranchSave")]
        //[HttpPost]
        //// GET :  /api/common/BankBranchSave
        //public IHttpActionResult BankBranchSave([FromBody] RF_BANK_BRANCHModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

        //[Route("BankAccountAutoList")]
        //[HttpGet]
        //public IHttpActionResult BankAccountAutoList(string pIS_EMP_ACC, string pBK_ACC_NO, int? pRF_BANK_ID)
        //{
        //    var obList = new ACC_BK_ACCOUNTModel().BankAccountAutoList(pIS_EMP_ACC, pBK_ACC_NO, pRF_BANK_ID);
        //    return Ok(obList);
        //}





        //[Route("GetAccPayPeriod")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/GetAccPayPeriod
        //public IHttpActionResult GetAccPayPeriod(int? pHR_COMPANY_ID = null, int? pHR_PERIOD_TYPE_ID = null, string pIS_CLOSED = null, string pIS_SHOW4_RPT = null)
        //{
        //    try
        //    {
        //        var obList = new ACC_PAY_PERIODModel().GetAccPayPeriod(pHR_COMPANY_ID, pHR_PERIOD_TYPE_ID, pIS_CLOSED, pIS_SHOW4_RPT);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("GetPayPeriodType")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/GetPayPeriodType
        //public IHttpActionResult GetPayPeriodType()
        //{
        //    try
        //    {
        //        var obList = new HrPeriodTypeModel().PeriodTypeListData();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("GetPayFiscalYear")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/GetPayFiscalYear
        //public IHttpActionResult GetPayFiscalYear(string pIS_CLOSED = null)
        //{
        //    try
        //    {
        //        var obList = new RF_FISCAL_YEARModel().FiscalYearData(pIS_CLOSED);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

       

        //[Route("GetIncrimentType")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/GetIncrimentType
        //public IHttpActionResult GetIncrimentType()
        //{
        //    try
        //    {
        //        var obList = new RF_INCR_TYPEModel().GetIncrimentType();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

       
    



        //[Route("getLineLoadingPlanData")]
        //[HttpGet]
        //// GET :  api/common/getLineLoadingPlanData?pHR_PROD_FLR_LST&pHR_PROD_LINE_LST
        //public IHttpActionResult getLineLoadingPlanData(String pHR_PROD_FLR_LST = null, String pHR_PROD_LINE_LST = null)
        //{
        //    try
        //    {
        //        var obList = new GMT_LN_LOAD_PLANModel().SelectAll(pHR_PROD_FLR_LST, pHR_PROD_LINE_LST);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}
        //[Route("getLineLoadingPlanDataEntry")]
        //[HttpGet]
        //// GET :  api/common/getLineLoadingPlanDataEntry?pHR_PROD_FLR_LST&pHR_PROD_LINE_LST&pPROD_DT
        //public IHttpActionResult getLineLoadingPlanDataEntry(String pHR_PROD_FLR_LST = null, String pHR_PROD_LINE_LST = null, DateTime? pPROD_DT = null)
        //{
        //    try
        //    {
        //        var obList = new GMT_LN_LOAD_PLANModel().getLineLoadingPlanDataEntry(pHR_PROD_FLR_LST, pHR_PROD_LINE_LST, pPROD_DT);

        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("getSewingProductionDashBoard")]
        //[HttpGet]
        //// GET :  api/common/getSewingProductionDashBoard?pHR_PROD_FLR_LST&pPROD_DT
        //public IHttpActionResult getSewingProductionDashBoard(String pHR_PROD_FLR_LST = null, DateTime? pPROD_DT = null)
        //{
        //    try
        //    {
        //        var obList = new HR_PROD_LINEModel().getSewingProdDashBoard(pHR_PROD_FLR_LST, pPROD_DT);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("SaveLineLoadingPlanData")]
        //[HttpPost]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/SaveLineLoadingPlanData
        //public IHttpActionResult SaveLineLoadingPlanData([FromBody] GMT_LN_LOAD_PLANModel ob)
        //{
        //    try
        //    {
        //        var obList = ob.Save();
        //        Hub.Clients.All.executedFromServer();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("SaveFinishingData")]
        //[HttpPost]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/SaveFinishingData
        //public IHttpActionResult SaveFinishingData([FromBody] GMT_FIN_PRODModel ob)
        //{
        //    try
        //    {
        //        var obList = ob.Save();
        //        Hub.Clients.All.executedFromServer();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("savePerformanceFaultReason")]
        //[HttpPost]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/savePerformanceFaultReason
        //public IHttpActionResult savePerformanceFaultReason([FromBody] RF_PFLT_RSN_TYPEModel ob)
        //{
        //    try
        //    {
        //        var obList = ob.Save();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}




        //[Route("getOrderStyleDropDownData")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID&pORDER_NO
        //public IHttpActionResult getOrderStyleDropDownData(
        //      Int64? pMC_BYR_ACC_ID = null,
        //      String pORDER_NO = null,
        //      Int64? pMC_ORDER_H_ID = null,
        //      DateTime? pFIRSTDATE = null,
        //      DateTime? pLASTDATE = null,
        //      int? pOption = 3000,
        //      Int64? pRF_FAB_PROD_CAT_ID = null
        //    )
        //{
        //    try
        //    {
        //        var obList = new MC_ORDER_STYLModel().getOrderStyleDropDownData(pMC_BYR_ACC_ID, pORDER_NO, pMC_ORDER_H_ID, pFIRSTDATE, pLASTDATE, pOption, pRF_FAB_PROD_CAT_ID);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("getOrderStyleDropDownDataForPln")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/getOrderStyleDropDownDataForPln
        //public IHttpActionResult getOrderStyleDropDownDataForPln(
        //      Int64? pMC_BYR_ACC_ID = null,
        //      String pORDER_NO = null,
        //      DateTime? pFIRSTDATE = null,
        //      DateTime? pLASTDATE = null,
        //      Int64? pINV_ITEM_CAT_ID_P = null,
        //      Int64? pINV_ITEM_CAT_ID =null,
        //      Int64? pLK_ORD_TYPE_ID = null
        //    )
        //{
        //    try
        //    {
        //        var obList = new MC_ORDER_STYLModel().getOrderStyleDropDownDataForPln(pMC_BYR_ACC_ID, pORDER_NO, pFIRSTDATE, pLASTDATE, pINV_ITEM_CAT_ID_P, pINV_ITEM_CAT_ID, pLK_ORD_TYPE_ID);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}



        //[Route("getOrderStyleItemDropDownData")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/getOrderStyleItemDropDownData?pMC_ORDER_H_ID&pITEM_NAME_EN
        //public IHttpActionResult getOrderStyleItemDropDownData(Int64? pMC_ORDER_H_ID = null, String pITEM_NAME_EN = null)
        //{
        //    try
        //    {
        //        var obList = new MC_ORDER_STYLModel().getOrderStyleItemDropDownData(pMC_ORDER_H_ID, pITEM_NAME_EN);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("getPerformanceFaultReasonData")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/getPerformanceFaultReasonData
        //public IHttpActionResult getPerformanceFaultReasonData()
        //{
        //    try
        //    {
        //        var obList = new RF_PFLT_RSN_TYPEModel().SelectAll();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}


        //[Route("getDyeDfctTypeList")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/getDyeDfctTypeList
        //public IHttpActionResult getDyeDfctTypeList()
        //{
        //    try
        //    {
        //        var obList = new RF_DY_DFCT_TYPEModel().SelectAll();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}



        //[Route("NoOfWorkingDay")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/NoOfWorkingDay?pHR_COMPANY_ID&pFROM_DT&pTO_DT
        //public IHttpActionResult getNoOfWorkingDay(int pHR_COMPANY_ID, DateTime? pFROM_DT, DateTime? pTO_DT)
        //{
        //    try
        //    {
        //        var obList = new HrYrlyCalndrModel().getNoOfWorkingDay(pHR_COMPANY_ID, pFROM_DT, pTO_DT);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("GetPendingReqCountH")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/GetPendingReqCountH
        //public IHttpActionResult getPendingReqCountH()
        //{
        //    try
        //    {
        //        var obList = new RF_REQ_TYPEModel().getPendingReqCountH();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("GetPendingReqCountHD")]
        //[HttpGet]
        //[System.Web.Http.Authorize]
        //// GET :  api/common/GetPendingReqCountHD?pRF_REQ_TYPE_ID
        //public IHttpActionResult getPendingReqCountHD(Int64 pRF_REQ_TYPE_ID)
        //{
        //    try
        //    {
        //        var obList = new RF_REQ_TYPEModel().getPendingReqCountHD(pRF_REQ_TYPE_ID);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("FindTnaProductionData")]
        //[HttpGet]
        //// GET :  api/common/FindTnaProductionData
        //public IHttpActionResult FindTnaProductionData()
        //{
        //    try
        //    {
        //        var obList = new KNT_BUYER_SHIP_MONTHModel().SelectAll();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("getLabelPrinter")]
        //[HttpGet]
        //// GET :  api/common/getLabelPrinter
        //public IHttpActionResult getLabelPrinter()
        //{
        //    try
        //    {
        //        var obList = new SC_RLBL_PRNTR_CFGModel().QueryDatas();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}


        //[Route("getRespDeptList")]
        //[HttpGet]
        //// GET :  api/common/getRespDeptList
        //public IHttpActionResult getRespDeptList()
        //{
        //    try
        //    {
        //        var obList = new RF_RESP_DEPTModel().getRespDeptList();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("SaveSrtFabBkRespDept")]
        //[HttpPost]
        //// GET :  api/common/SaveSrtFabBkRespDept
        //public IHttpActionResult SaveSrtFabBkRespDept([FromBody] RF_RESP_DEPTModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

        //[Route("getSrtFabBkReasonTyp")]
        //[HttpGet]
        //// GET :  api/common/getSrtFabBkReasonTyp
        //public IHttpActionResult getSrtFabBkReasonTyp()
        //{
        //    try
        //    {
        //        var obList = new RF_SFAB_RSN_TYPEModel().getSrtFabBkReasonTyp();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("SaveSrtFabBkReasonTyp")]
        //[HttpPost]
        //// GET :  api/common/SaveSrtFabBkReasonTyp
        //public IHttpActionResult SaveSrtFabBkReasonTyp([FromBody] RF_SFAB_RSN_TYPEModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

        //[Route("SaveCompPayPeriod")]
        //[HttpPost]
        //// GET :  api/common/SaveCompPayPeriod
        //public IHttpActionResult SaveCompPayPeriod([FromBody] ACC_PAY_PERIODModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

        //[Route("SaveGmtPart")]
        //[HttpPost]
        //// GET :  api/common/SaveGmtPart
        //public IHttpActionResult SaveGmtPart([FromBody] RF_GARM_PARTModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

        //[Route("getCompanyInsuranceList")]
        //[HttpGet]
        //// GET :  api/common/getCompanyInsuranceList
        //public IHttpActionResult getCompanyInsuranceList()
        //{
        //    try
        //    {
        //        var obList = new RF_INSURN_COMPModel().SelectAll();
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}



        //[Route("GetUploadDocList")]
        //[HttpGet]
        //// GET :  /api/common/GetUploadDocList
        //public IHttpActionResult GetUploadDocList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pDOC_REF_NO = null,
        //    string pSTYLE_NO = null, string pORDER_NO = null)
        //{
        //    try
        //    {
        //        var obList = new RF_DOC_ARCVModel().SelectAll(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID, pDOC_REF_NO, pSTYLE_NO, pORDER_NO);
        //        return Ok(obList);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        //[Route("DeleteUploadedOtherDocs")]
        //[HttpPost]
        //// POST :  /api/common/DeleteUploadedOtherDocs
        //public IHttpActionResult DeleteUploadedOtherDocs(RF_DOC_ARCVModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Delete();

        //            string vMsg = jsonStr.Substring(9, 9);
        //            if (vMsg == "MULTI-001")
        //            {
        //                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/OTHER_DOCS"), ob.DOC_PATH_URL);
        //                System.IO.File.Delete(path);

        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}




    }
}
