using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class TransactionStep
    {
        [Key]
        public long TransactionStepID { get; set; }

        public Step Step { get; set; }
        public long StepID { get; set; }

        public RazorPay_Attribute RazorPay_Attribute { get; set; }
        public long RazorPay_AttributeID { get; set; }

        public string PayeeName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string ProductCode { get; set; }
        public string CustomerCode { get; set; }

        public string ApplicationUrl { get; set; }
        public string SucessUrl { get; set; }
        public string ErrorUrl { get; set; }

        public decimal Amount { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string SuscriptionNumber { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
