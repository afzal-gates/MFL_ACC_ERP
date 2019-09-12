using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution.Areas.Accounting.Models
{
    public class VoucherMasterViewModel
    {
        public ACC_VOUCHER_MASTER ACC_VOUCHER_MASTER { get; set; }
        public ACC_TEMP_VOUCHER_DETAIL ACC_VOUCHER_DETAIL { get; set; }
        public List<SelectModel> VoucherTypes { get; set; }
        public List<SelectModel> CostCenters { get; set; }
   
        public object PaymentModes { get; set; }
        public List<ACC_CURRENCY> Currencies { get; set; }
        public List<ACC_VOUCHER_DETAIL> VoucherDetails { get; set; }
        public ACC_COMPANY ACC_COMPANY { get; set; }
        public VoucherMasterViewModel()
        {
            this.ACC_VOUCHER_DETAIL = new ACC_TEMP_VOUCHER_DETAIL();
            this.PaymentModes = new List<object>();
            this.Currencies = new List<ACC_CURRENCY>();
           
        }
    }
}