using ERP.BLL;
using ERP.Core;
using ERP.Model.Accounting;
using System.Collections.Generic;
using System.Web.Http;

namespace ERPSolution.Areas.Accounting.Api
{
    [RoutePrefix("api/accounting/companies")]
    public class CompaniesController : BaseApiController
    {

        private readonly ICompanyService _companyService;
        public CompaniesController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }
        [Route("get-companies")]
        [HttpGet]
        public IHttpActionResult GetCompanies()
        {
            return Ok(new ResponseMessage<List<ACC_COMPANY>>()
            {
                Result = _companyService.GetCompanies()
            });

        }

        [Route("save-company")]
        [HttpPost]
        [ModelValidation]
        public IHttpActionResult SaveCompany([FromBody]ACC_COMPANY model)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _companyService.SaveCompany(0, model)
            });

        }

        [Route("update-company")]
        [HttpPut]
        [ModelValidation]
        public IHttpActionResult UpdateCompany(int id, [FromBody]ACC_COMPANY model)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _companyService.SaveCompany(id, model)
            });
        }


        [Route("get-company")]
        [HttpGet]
        public IHttpActionResult GetCompany(int id)
        {
            return Ok(new ResponseMessage<ACC_COMPANY>()
            {
                Result = _companyService.GetCompanyById(id)
            });

        }

        [Route("delete-company")]
        [HttpDelete]
        public IHttpActionResult DeleteCompany(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = _companyService.DeleteCompany(id)
            });

        }
    }
}
