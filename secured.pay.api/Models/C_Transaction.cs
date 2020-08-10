using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class C_Transaction
    {
        [Key]
        public long C_TransactionID { get; set; }

        public string ProductCode { get; set; }
        public string CustomerCode { get; set; }

        public string PaymentID { get; set; }
        public string OrderID { get; set; }
        public string Signature { get; set; }
        public decimal Amount { get; set; }
        public string PayeeName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public long PaymentStatusID { get; set; }

        public PaymentType PaymentType { get; set; }
        public long PaymentTypeID { get; set; }

        public TransactionStep TransactionStep { get; set; }
        public long? TransactionStepID { get; set; }


        public DateTime TransactionDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
