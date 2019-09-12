using ERP.Model.Accounting;
using ERP.Shared;
using System.Collections.Generic;

namespace ERP.Data
{
   public interface ICostCenterRepository
    {
        List<ACC_COST_CENTER> GetAll(string compCode);
        ACC_COST_CENTER GetById(int id);
        bool Save(int id, ACC_COST_CENTER model);
        bool Delete(int id);
        string GetNewId(string comp_code);
        List<SelectModel> GetCostCenterSelectModels(string comp_code);
    }
}
