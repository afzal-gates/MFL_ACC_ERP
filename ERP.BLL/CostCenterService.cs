using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.BLL
{
    public class CostCenterService : ICostCenterService
    {
       private readonly ICostCenterRepository costCenterRepository;
        public CostCenterService(ICostCenterRepository costCenterRepository)
        {
            this.costCenterRepository = costCenterRepository;
        }

        public List<ACC_COST_CENTER> GetCostCenters(string searchText, int pn, int ps, out int total)
        {
            List<ACC_COST_CENTER> costCenters= costCenterRepository.GetAll(CompanyCode.comp_code);
            total = costCenters.Count();
            return costCenters = costCenters.OrderBy(x=>x.GR_NAME).Skip((pn - 1) * ps).Take(ps).ToList();
        }

        public ACC_COST_CENTER GetCostCenterById(int id)
        {
            ACC_COST_CENTER model = new ACC_COST_CENTER();
            if (id <=0)
            {
                model.COST_CENTER_CODE = costCenterRepository.GetNewId(CompanyCode.comp_code);
            }
            else
            {
                model = costCenterRepository.GetById(id);
            }
            return model;
        }

        public ACC_COST_CENTER SaveCostCenter(int id,ACC_COST_CENTER model)
        {
            model.COMP_CODE = CompanyCode.comp_code;

            if (id <= 0)
            {
                model.COST_CENTER_CODE = costCenterRepository.GetNewId(model.COMP_CODE);
            }
            costCenterRepository.Save(id, model);
            return model;
        }
        public bool DeleteCostCenter(int id)
        {
            return costCenterRepository.Delete(id);
        }

        public List<SelectModel> GetCostCenterSelectModels(string comp_code)
        {
            List<SelectModel> list= costCenterRepository.GetCostCenterSelectModels(comp_code);
            return list;
        }
    }
}
