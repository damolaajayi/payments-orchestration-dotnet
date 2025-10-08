using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Domain.Abstractions
{
    public class DomainEntity : IHasDomainEvents
    {
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.ToArray();

        protected void AddDomainEvent(IDomainEvent @event)
            => _domainEvents.Enqueue(@event);
        public void ClearDomainEvents()
        {
            while (_domainEvents.TryDequeue(out _)) { }
        }
    }
}
