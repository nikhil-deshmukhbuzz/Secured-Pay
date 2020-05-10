using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secured.pay.api.Enums
{
    public enum PaymentType
    {
        Cash = 1,
        Cheque = 2,
        Online = 3,
        PaymentGateway = 4,
        Other = 5,
    }
}
