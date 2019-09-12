using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ERP.Shared;

namespace ERP.Data
{
   public interface IMainClassRepository
    {

        List<ACC_MAIN_CLASSModel> GetAll(string compId,string accode);
        ACC_MAIN_CLASSModel GetById(int id);
        string Save(ACC_MAIN_CLASSModel vtm);
        string Delete();
        string NextCode(string accode,string compId);

        List<SelectModel> GetCashBankHeads(string compCode, string mapCode);
       bool IsMainClassExist(string compCode, string mainName,long id);
    }
}
