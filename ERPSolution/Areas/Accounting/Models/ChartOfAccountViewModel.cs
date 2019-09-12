using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution.Areas.Accounting.Models
{
    public class ChartOfAccountViewModel
    {
        public IEnumerable<TreeView> TreeViews { get; set; }
      
    }
}