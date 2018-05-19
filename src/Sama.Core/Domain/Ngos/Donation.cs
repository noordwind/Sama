using System;

namespace Sama.Core.Domain.Ngos
{
    public class Donation : Entity
    {
        public Guid UserId { get; protected set; }
        public decimal Value { get; protected set; }
        public string Hash { get; protected set; }
        public DateTime CreatedAt { get; protected set; } 

        protected Donation()
        {
        }

        public Donation(Guid id, Guid userId, decimal value, string hash) : base(id)
        {
            UserId = userId;
            Value = value;
            Hash = hash;
            CreatedAt = DateTime.UtcNow;
        }
    }
}