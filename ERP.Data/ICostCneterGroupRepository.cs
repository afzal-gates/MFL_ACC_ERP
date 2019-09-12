using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
   public interface ICostCneterGroupRepository
    {
        List<ACC_COST_CENTER_GROUP> GetAll(string compCode);
        ACC_COST_CENTER_GROUP GetById(int id);
        bool Save(int id, ACC_COST_CENTER_GROUP model);
        bool Delete(int id);
    }
}
