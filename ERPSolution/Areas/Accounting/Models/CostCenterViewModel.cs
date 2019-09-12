using ERP.Model.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPSolution.Areas.Accounting.Models
{
    public class CostCenterViewModel
    {
        public ACC_COST_CENTER CostCenter { get; set; }
        public List<ACC_COST_CENTER_GROUP> Groups { get; set; }

        public CostCenterViewModel()
        {
            CostCenter = new ACC_COST_CENTER();
            Groups = new List<ACC_COST_CENTER_GROUP>();
        }
    }
}