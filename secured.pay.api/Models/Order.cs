using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class OrderRequest
    {
        public long TransactionStepID { get; set; }
        public string PayeeName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CustomerCode { get; set; }
        public string ProductCode { get; set; }
        public decimal Amount { get; set; }
        public string ApplicationUrl { get; set; }
        public string SucessUrl { get; set; }
        public string ErrorUrl { get; set; }

        public string PaymentID { get; set; }
        public string OrderID { get; set; }
        public string Signature { get; set; }
        public string SuscriptionNumber { get; set; }
    }

    public class OrderResponse
    {
        public string RedirectUrl { get; set; }
        public string Status { get; set; }
        public string Key { get; set; }
        public long TransactionStepID { get; set; }

        public object Response { get; set; }
    }

    public class TransactionResponse
    {
        public string ProductCode { get; set; }
        public string CustomerCode { get; set; }

        public string PaymentID { get; set; }
        public string OrderID { get; set; }
        public string Signature { get; set; }
        public decimal Amount { get; set; }
        public string PayeeName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentType { get; set; }
        public string TransactionStep { get; set; }
        public string SuscriptionNumber { get; set; }
        public int? ValidityInMonth { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
