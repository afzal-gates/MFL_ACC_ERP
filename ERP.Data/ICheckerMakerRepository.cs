using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data
{
    public interface ICheckerMakerRepository
    {
        int UpdateCheckerMaker(string comp_code);
    }
}
