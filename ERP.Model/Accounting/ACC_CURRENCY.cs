using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
    public class ACC_CURRENCY
    {

       public Int64 CURRENCY_ID { get; set; }
        public string SYMBOL { get; set; }
        public string FORMAL_NAME { get; set; }
        public string REMARKS { get; set; }
        public string COMP_CODE { get; set; }
        public double RATE { get; set; }

    }
}
