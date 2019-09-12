using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
    public class ACC_PAYMENT_MODE
    {
        public Int64 PAYMENT_MODE_ID { get; set; }
        public string PM_NAME { get; set; }
        public string REF_CODE { get; set; }
        public string COMP_CODE { get; set; }
        public string REMARKS { get; set; }
        public string SHORT_NAME { get; set; }
    }
}
