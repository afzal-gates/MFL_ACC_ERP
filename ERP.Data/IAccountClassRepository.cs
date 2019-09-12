using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public interface IAccountClassRepository
    {

        List<ACC_AC_CLASSModel> GetAll(string compId, string acgroup);
        ACC_AC_CLASSModel GetById(int id);
        string Save(ACC_AC_CLASSModel vtm);
        string Delete();
    }
}
