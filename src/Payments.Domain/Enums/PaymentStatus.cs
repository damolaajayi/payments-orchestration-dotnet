using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Enums
{
    public enum PaymentStatus
    {
        Created = 0,
        Authorized = 1,
        Captured = 2,
        Refunded = 3,
        Failed = 4,
        Canceled = 5
    }
}
