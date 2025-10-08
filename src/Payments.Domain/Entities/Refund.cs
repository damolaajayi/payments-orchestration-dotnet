using Payments.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Entities
{
    public sealed class Refund
    {
        public Guid Id { get; private set; }
        public Guid PaymentId { get; private set; }
        public Money Amount { get; private set; }
        public string Reason { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        private Refund() { }

        private Refund(Guid id, Guid paymentId, Money amount, string reason)
        {
            if (amount.Amount<= 0) throw new ArgumentOutOfRangeException(nameof(amount));
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            PaymentId = paymentId;
            Amount = amount;
            Reason = string.IsNullOrWhiteSpace(reason) ? "unspecified" : reason;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Refund Create(Guid paymentId, Money amount, string reason)
            => new(Guid.NewGuid(), paymentId, amount, reason);
    }
}
