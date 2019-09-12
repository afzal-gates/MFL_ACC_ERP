using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
   public class ACC_VOUCHER_DETAIL
    {
        public Int64 VOUCHER_DETAIL_ID { get; set; }
        public Int64 VOUCHER_MASTER_ID { get; set; }
        public string AC_CODE { get; set; }
        public string MAIN_CODE { get; set; }
        public string SUB_CODE { get; set; }
        public double CR_AMT { get; set; }
        public double DR_AMT { get; set; }
        public Int64 COST_CENTER_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string M_CODE { get; set; }
        public string XSTATUS { get; set; }
        public string COMP_CODE { get; set; }
        public string EXCHANGE_RATE { get; set; }
        public Int64 CURRENCY_ID { get; set; }
        public string BILL_REF_ID { get; set; }
        public string BILL_DETAIL_ID { get; set; }
        public string BILL_NO { get; set; }


    }
}
