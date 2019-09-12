using ERP.Model;
using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public interface IChartOfAccountRepository
    {
        List<ACC_AC_CLASSModel> GetAll();
        ACC_AC_CLASSModel GetById(int id);
        string Save(ACC_AC_CLASSModel vtm);
        string Delete();
    }
}
