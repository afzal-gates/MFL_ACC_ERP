using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;
using ERP.Shared;

namespace ERP.BLL
{
    public interface IUnilayerChartofAccountService
    {

        IEnumerable<TreeView> GetChartOfAccount();
        TreeView SaveChartOfAccount(TreeView model);
        TreeView UpdateChartOfAccount(string code, string controlCode, TreeView model);
        List<ACC_SUB_CLASSModel> GetAccountHeards(string compCode, string searchKey);
        TreeView GetMainClassHeads(string compCode);
        IEnumerable<TreeView> GetControlChartOfAccounts();
        List<SelectModel> GetCashBankHeads(string compCode, string cashMapCode);

        bool CheckGlTransactionExist(string compCode, string glCode, string control_code);
        bool DeleteGlAccount(string compCode, string glCode, string controlCode, int id);
    }
}
