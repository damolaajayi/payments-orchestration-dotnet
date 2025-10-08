using Payments.Domain.Abstractions;
using Payments.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Events
{
    public sealed record PaymentAuthorized(Guid PaymentId, string Provider, string ProviderPaymentId, Money Amount) : IDomainEvent
    {
        public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
    }
}
