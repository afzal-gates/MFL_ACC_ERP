using ERP.Model.Accounting;
using ERP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution.Areas.Accounting.Models
{
    public class StockCloasingViewModel
    {
        public List<ACC_STOCK_CLOSING> Stocks { get; set; }
        public List<int> Years { get; set; }
        public List<SelectModel> Months { get; set; }
    }
}