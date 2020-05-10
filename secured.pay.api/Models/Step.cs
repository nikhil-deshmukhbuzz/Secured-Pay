using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class Step
    {
        [Key]
        public long StepID { get; set; }
        public string Name { get; set; }
    }
}
