using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Model.Accounting;

namespace ERP.Data
{
    public interface IMapClassRepository
    {
        List<ACC_MAP_CLASS> GetAll(string compCode, string accode, string mainCode, string parentCode);
        string NextCode(string compCode);
        string Save(ACC_MAP_CLASS mapClass);
        bool IsMapClassExist(string compCode, string mapName,long id);
        ACC_SUB_CLASSModel GetTreeEx();
    }
}
