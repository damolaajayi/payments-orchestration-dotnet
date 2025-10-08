using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.ValueObjects
{
    public readonly record struct Currency(string Code)
    {
        public static readonly Currency USD = new("USD");
        public static readonly Currency EUR = new("EUR");
        public static readonly Currency GBP = new("GBP");
        public static readonly Currency NGN = new("NGN");


        public override string ToString() => Code;
    }
}
