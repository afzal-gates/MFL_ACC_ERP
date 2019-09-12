using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
    public class ACC_TEMP_VOUCHER_DETAIL
    {
        public string COMP_CODE { get; set; }
        public string AC_CODE { get; set; }
        public string MAIN_CODE { get; set; }
        [Required(ErrorMessage = "Please Select Account Head")]
        public string SUB_CODE { get; set; }
        [Required(ErrorMessage ="Please Select Account Head")]
        public string SUB_NAME { get; set; }
        public double DR_AMT { get; set; }
        public double CR_AMT { get; set; }
        public string DESCRIPTION { get; set; }
        public string XSTATUS { get; set; }
        public string MAP_CODE { get; set; }
        public string USER_ID { get; set; }
        [Required(ErrorMessage = "Please Select Cost Center")]
        public Int64 COST_CENTER_ID { get; set; }
        public Int64 TEMP_ID { get; set; }
        public string COST_CENTER_NAME { get; set; }
        public Int64 VOUCHER_TYPE_ID { get; set; }
        public double EXCHANGE_RATE { get; set; }
        public Int64 CURRENCY_ID { get; set; }
        public string BILL_REF_ID { get; set; }
        public string BILL_NO { get; set; }
        public string BILL_DETAIL_ID { get; set; }

    }
}
