using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.BLL
{
    public interface ICompanyService
    {
        List<ACC_COMPANY> GetCompanies();
        bool SaveCompany(int id, ACC_COMPANY model);
        ACC_COMPANY GetCompanyById(int id);
        bool DeleteCompany(int id);
    }
}
