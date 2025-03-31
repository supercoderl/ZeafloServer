using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Entities
{
    public abstract class Entity<TId> where TId : IEquatable<TId>
    {
        public TId Id { get; private set; } = default!;
        public bool Deleted { get; private set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity() { }

        public void SetId(TId id)
        {
            if (id is null || (id is Guid guid && guid == Guid.Empty) || (id is string str && string.IsNullOrWhiteSpace(str)))
            {
                throw new ArgumentException($"{nameof(id)} may not be null, empty or default.");
            }

            Id = id;
        }

        public void Delete()
        {
            Deleted = true;
        }

        public void Undelete()
        {
            Deleted = false;
        }
    }
}
