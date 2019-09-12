using ERP.Data;
using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
   public class CostCenterGroupService: ICostCenterGroupService
    {
        private readonly CostCneterGroupRepository costCenterGroupRepository;
        public CostCenterGroupService(CostCneterGroupRepository costCenterGroupRepository)
        {
            this.costCenterGroupRepository = costCenterGroupRepository;
        }
        public List<ACC_COST_CENTER_GROUP> GetCostCenterGroups()
        {
            return costCenterGroupRepository.GetAll(CompanyCode.comp_code);
        }

        public ACC_COST_CENTER_GROUP GetCostCenterGroupById(int id)
        {
           
            return costCenterGroupRepository.GetById(id); ;
        }

        public ACC_COST_CENTER_GROUP SaveCostCenterGroup(int id, ACC_COST_CENTER_GROUP model)
        {

            costCenterGroupRepository.Save(id, model);
            return model;
        }
        public bool DeleteCostCenterGroup(int id)
        {
            return costCenterGroupRepository.Delete(id);
        }

      
    }
}
