using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.ValueObjects
{
    public readonly record struct Money(decimal Amount, Currency Currency)
    {
        public static Money Zero(Currency c) => new(0m, c);

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            return new(Amount - other.Amount, Currency);
        }

        public Money Negate() => new(-Amount, Currency);

        public bool IsNegative => Amount < 0;

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot operate on Money values with different currencies.");
        }

        public static Money operator +(Money a, Money b) => a.Add(b);
        public static Money operator -(Money a, Money b) => a.Subtract(b);
    }
}
