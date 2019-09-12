using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
   public class ACC_VOUCHER_MASTER
    {
        public Int64 VOUCHER_MASTER_ID { get; set; }
        public string POST_ID { get; set; }
        [Required(ErrorMessage ="Voucher Data Missing")]
        public DateTime POST_DATE { get; set; }
        [Required(ErrorMessage = "Voucher No Missing")]
        public string VOUCHER_NO { get; set; }
        [Required(ErrorMessage = "Voucher Type Missing")]
        [Range(1, Int64.MaxValue,ErrorMessage = "Voucher Type Missing")]
        public Int64 VOUCHER_TYPE_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string EMPLOYEE_ID { get; set; }
        public string REF_ID { get; set; }
        public string DIN { get; set; }
        public string CHQ_NO { get; set; }
        public DateTime CHQ_DATE { get; set; }
        public string COMP_CODE { get; set; }
        public string CREATED_BY { get; set; }
        [Required(ErrorMessage = "Voucher Date Missing")]
       // [Range(typeof(DateTime), "1/1/1966", "1/1/1966", ErrorMessage ="Voucher Date Missing !")]
        public DateTime CREATED_DATE { get; set; }
        public Int64 PAYMENT_MODE_ID { get; set; }
        public string IS_POSTED { get; set; }
        public string PAY_TYPE { get; set; }
        public string ACNT_NAME { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

    }
}
