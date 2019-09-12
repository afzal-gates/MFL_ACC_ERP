using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.BLL
{
   public interface IChartOfAccountService
    {
        IEnumerable<TreeView> GetChartOfAccount();
        TreeView SaveChartOfAccount(TreeView model);
        TreeView UpdateChartOfAccount(string code, string controlCode, TreeView model);
        List<ACC_SUB_CLASSModel> GetAccountHeards( string compCode, string searchKey);
       TreeView GetMainClassHeads(string compCode);
    }
}
