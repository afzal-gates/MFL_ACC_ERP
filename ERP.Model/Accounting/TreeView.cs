using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Accounting
{
   public  class TreeView
    {
        public Int64 recordId { get; set; }
        public string controlCode { get; set; }
        public string parentCode { get; set; }
        public string code { get; set; }
        public string mapCode { get; set; }
        public string text { get; set; }
        public bool expanded { get; set; }
        public List<TreeView> items { get; set; }
       
    }
}
