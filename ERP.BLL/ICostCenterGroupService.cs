using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
   public interface ICostCenterGroupService
    {
        List<ACC_COST_CENTER_GROUP> GetCostCenterGroups();
        ACC_COST_CENTER_GROUP GetCostCenterGroupById(int id);
        ACC_COST_CENTER_GROUP SaveCostCenterGroup(int id, ACC_COST_CENTER_GROUP model);
        bool DeleteCostCenterGroup(int id);
    }
}
