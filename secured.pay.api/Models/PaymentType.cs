using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class PaymentType
    {
        [Key]
        public long PaymentTypeID { get; set; }
        public string Type { get; set; }
    }
}
