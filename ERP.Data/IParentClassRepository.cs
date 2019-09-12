using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
   public interface IParentClassRepository
    {

        List<ACC_AC_PARENT_CLASSModel> GetAll(string compId);
        ACC_AC_PARENT_CLASSModel GetById(int id);
        string Save(ACC_AC_PARENT_CLASSModel vtm);
        string Delete();
    }
}
