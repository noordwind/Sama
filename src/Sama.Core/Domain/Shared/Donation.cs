using System;

namespace Sama.Core.Domain.Shared
{
    public class Donation : Entity
    {
        public Guid UserId { get; protected set; }
        public Guid NgoId { get; protected set; }
        public string NgoName { get; protected set; }
        public decimal Value { get; protected set; }
        public string Hash { get; protected set; }
        public DateTime CreatedAt { get; protected set; } 

        protected Donation()
        {
        }

        public Donation(Guid id, Guid userId, Guid ngoId, string ngoName,
            decimal value, string hash) : base(id)
        {
            UserId = userId;
            NgoId = ngoId;
            NgoName = ngoName;
            Value = value;
            Hash = hash;
            CreatedAt = DateTime.UtcNow;
        }
    }
}