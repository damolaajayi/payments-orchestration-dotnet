using Payments.Domain.ValueObjects;
using Payments.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Events
{
    public sealed record PaymentRefunded(Guid PaymentId, Money Amount, string Reason) : IDomainEvent
    {
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    }
}
