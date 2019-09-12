using ERP.Data;
using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
   public class CompanyService: ICompanyService
    {
        private readonly ICompanyRepository companyRepository;
        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public bool DeleteCompany(int id)
        {
           return companyRepository.Delete(id);
        }

        public List<ACC_COMPANY> GetCompanies()
        {
            return companyRepository.GetAll();
        }

        public ACC_COMPANY GetCompanyById(int id)
        {

            ACC_COMPANY model = new ACC_COMPANY();
            if (id <= 0)
            {
                model.COMP_CODE = companyRepository.GetNewId();
            }
            else
            {
                model = companyRepository.GetById(id);
            }
            return model;

        }

        public bool SaveCompany(int id, ACC_COMPANY model)
        {
            if (id <= 0)
            {
                model.COMP_CODE = companyRepository.GetNewId();
            }
            return companyRepository.Save(id, model);
        }
    }
}
