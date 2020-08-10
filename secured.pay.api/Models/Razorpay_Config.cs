using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class Razorpay_Config
    {
        [Key]
        public long Razorpay_ConfigID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Enviorment { get; set; }
        public int Unit { get; set; }
        public bool IsActive { get; set; }
    }
}
