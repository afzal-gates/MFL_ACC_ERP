using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
   public class ACC_COMPANY
    {
        public Int64 COMPANY_ID { get; set; }
        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string PREFIX { get; set; }
    }
}
