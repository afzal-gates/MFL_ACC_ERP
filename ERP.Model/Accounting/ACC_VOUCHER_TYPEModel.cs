using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class ACC_VOUCHER_TYPEModel
    {
        public Int64 VOUCHERTYPEID { get; set; }
        [Required]
        public string TYPENAME { get; set; }
        [Required]
        public string REMARKS { get; set; }

   
    }
}