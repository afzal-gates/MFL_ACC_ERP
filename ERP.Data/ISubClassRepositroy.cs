using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
    public interface ISubClassRepositroy
    {
        List<ACC_SUB_CLASSModel> GetAll(string compId, string acCode, string mainCode);
        ACC_SUB_CLASSModel GetById(int id);
        string Save(ACC_SUB_CLASSModel subclass);
        int Delete(string compCode, string glCode, int id);
        string NextCode(string parenetCode, string comp_code);
        List<ACC_SUB_CLASSModel> GetAccountHeards(string compCode, string searchKey);
        List<ACC_SUB_CLASSModel> GetAllByParentCode(string compCode, string pcode);
        bool IsSubClassExist(string compCode, string text,long id);
    }
}
