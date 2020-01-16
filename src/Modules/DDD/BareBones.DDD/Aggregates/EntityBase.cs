using System.Collections.Generic;
using BareBones.DDD.DomainEvents;

namespace BareBones.DDD.Aggregates
{
    public abstract class EntityBase<TEntityId>
    {
        int? _requestedHashCode;

        private TEntityId _Id;

        public virtual  TEntityId Id
        {
            get => _Id;
            protected set
            {
                _Id = value;
            }
        }

        private List<IDomainEvent> _domainEvents;
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return Id.Equals(default(TEntityId));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EntityBase<TEntityId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = (EntityBase<TEntityId>) obj;

            if (item.IsTransient() || IsTransient())
                return false;

            return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(EntityBase<TEntityId> left, EntityBase<TEntityId> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(EntityBase<TEntityId> left, EntityBase<TEntityId> right)
        {
            return !(left == right);
        }
    }
}
