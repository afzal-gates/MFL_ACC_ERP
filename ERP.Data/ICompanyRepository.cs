using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface ICompanyRepository
    {
        bool Delete(int id);
        List<ACC_COMPANY> GetAll();
        ACC_COMPANY GetById(int id);
        bool Save(int id, ACC_COMPANY model);
        string GetNewId();
    }
}
