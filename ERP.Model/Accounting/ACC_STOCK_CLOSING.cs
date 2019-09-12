using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
   public  class ACC_STOCK_CLOSING
    {
        [Required]
        public Int64 STOCK_CLOSING_ID { get; set; }
        public string AC_CODE { get; set; }
        public string MAIN_CODE { get; set; }
        public string SUB_CODE { get; set; }
        public string SUB_NAME { get; set; }
        public double OPENING { get; set; }
        public double DR { get; set; }
        public double CR { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Amount")]
        public double? CLOSING { get; set; }
        public double BALANCE { get; set; }
        public double NET_AMT { get; set; }
        public Int64 YEAR_CODE { get; set; }
        public Int64 MONTH_CODE { get; set; }
        public string COMP_CODE { get; set; }
    }
}
