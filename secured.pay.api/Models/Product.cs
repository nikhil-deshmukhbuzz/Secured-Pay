using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
