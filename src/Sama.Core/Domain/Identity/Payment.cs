using System;

namespace Sama.Core.Domain.Identity
{
    public class Payment : AggregateRoot
    {
        public Guid UserId { get; protected set; }
        public decimal Value { get; protected set; }
        public string Hash { get; protected set; }
        public DateTime CreatedAt { get; protected set; } 

        protected Payment()
        {
        }      

        public Payment(Guid id, Guid userId, decimal value, string hash) : base(id)
        {
            UserId = userId;
            Value = value;
            Hash = hash;
            CreatedAt = DateTime.UtcNow;
        }
    }
}