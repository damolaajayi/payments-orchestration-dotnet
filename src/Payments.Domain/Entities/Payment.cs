using Payments.Domain.Abstractions;
using Payments.Domain.Enums;
using Payments.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Entities
{
    public sealed class Payment : DomainEntity
    {
        public Guid Id { get; private set; }
        public Money Amount { get; private set; }
        public string CurrencyCode => Amount.Currency.Code;
        public PaymentStatus Status { get; private set; }
        public string MerchantReference { get; private set; }
        public string CustomerId { get; private set; }
        public string? Provider { get; private set; }
        public string? ProviderPaymentId { get; private set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        private Payment() { }

        private Payment(Guid id, Money amount, string merchantReference, string customerId)
        {
            if(amount.IsNegative)
                throw new ArgumentException("Amount cannot be negative.");
            Id = id;
            Amount = amount;
            MerchantReference = merchantReference;
            CustomerId = customerId;
            Status = PaymentStatus.Created;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Payment Create(Money amount, string merchantReference, string customerId)
        {
            var p = new Payment(Guid.NewGuid(), amount, merchantReference, customerId);
            return p;
        }

        public void MarkAuthorized(string provider, string providerPaymentId)
        {
            EnsureCanAuthorize();
            Provider = provider;
            ProviderPaymentId = providerPaymentId;
            Status = PaymentStatus.Authorized;
            Touch();
            AddDomainEvent(new Events.PaymentAuthorized(Id, Provider, ProviderPaymentId, Amount));
        }

        public void MarkCaptured()
        {
            EnsureCanCapture();
            Status = PaymentStatus.Captured;
            Touch();
            AddDomainEvent(new Events.PaymentCaptured(Id, Amount));
        }

        public Refund IssueRefund(Money amount, string reason)
        {
            EnsureCanRefund(amount);
            var refund = Refund.Create(Id, amount, reason);
            Touch();
            AddDomainEvent(new Events.PaymentRefunded(Id, amount, reason));
            return refund;
        }

        public void Fail(string reason)
        {
            if (Status is PaymentStatus.Captured or PaymentStatus.Refunded)
                throw new InvalidOperationException("Cannot fail a payment that has been captured or refunded.");
            Status = PaymentStatus.Failed;
            Touch();
            AddDomainEvent(new Events.PaymentFailed(Id, reason));
        }

        public void Cancel(string reason)
        {
            if(Status is PaymentStatus.Captured or PaymentStatus.Refunded)
                throw new InvalidOperationException("Cannot cancel a payment that has been captured or refunded.");
            Status = PaymentStatus.Canceled;
            Touch();
            AddDomainEvent(new Events.PaymentFailed(Id, $"Canceled :{reason}"));
        }

        private void EnsureCanAuthorize()
        {
            if (Status != PaymentStatus.Created)
                throw new InvalidOperationException("Only payments in 'Created' status can be authorized.");
        }

        private void EnsureCanCapture()
        {
            if (Status != PaymentStatus.Authorized)
                throw new InvalidOperationException("Only payments in 'Authorized' status can be captured.");
        }

        private void EnsureCanRefund(Money amount)
        {
            if(Status != PaymentStatus.Captured)
                throw new InvalidOperationException("Only captured payments can be refunded.");
            if(amount.Currency != Amount.Currency)
                throw new InvalidOperationException("Refund currency must match payment currency.");
            if(amount.Amount <= 0)
                throw new InvalidOperationException("Refund amount must be positive.");
            if(amount.Amount > Amount.Amount)
                throw new InvalidOperationException("Cannot refund more than captured amount.");
        }
        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
    }
}
