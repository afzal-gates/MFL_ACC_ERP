using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
    public interface ICostCenterService
    {
        List<ACC_COST_CENTER> GetCostCenters(string searchText, int pn, int ps, out int total);
        ACC_COST_CENTER GetCostCenterById(int id);
        ACC_COST_CENTER SaveCostCenter(int id,ACC_COST_CENTER model);
        bool DeleteCostCenter(int id);
        List<SelectModel> GetCostCenterSelectModels(string compCode);
    }
}
