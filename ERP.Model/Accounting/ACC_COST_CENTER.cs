using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
    public class ACC_COST_CENTER
    {
        public Int64 COST_CENTER_ID { get; set; }
        public string COST_CENTER_CODE { get; set; }
        public string COST_CENTER_NAME { get; set; }
        public string COMP_CODE { get; set; }
        public string REMARKS { get; set; }
        public string GR_NAME { get; set; }
        public Int64 GROUP_ID { get; set; }


    }
}
