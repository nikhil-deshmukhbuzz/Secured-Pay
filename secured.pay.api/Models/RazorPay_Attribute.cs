using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Models
{
    public class RazorPay_Attribute
    {
       [Key]
       public long RazorPay_AttributeID { get;set; }
       public string id                   { get;set; }
       public string entity               { get;set; }
       public string amount               { get;set; }
       public string amount_paid          { get;set; }
       public string amount_due           { get;set; }
       public string currency             { get;set; }
       public string receipt              { get;set; }
       public string offer_id             { get;set; }
       public string status               { get;set; }
       public string attempts             { get;set; }
       public string created_at           { get;set; }
       public string checkout_config_id   { get;set; }
    }
}
