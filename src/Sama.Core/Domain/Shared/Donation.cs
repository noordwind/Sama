using System;
using System.Linq;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Ngos;

namespace Sama.Core.Domain.Shared
{
    public class Donation : Entity
    {
        public Guid UserId { get; protected set; }
        public string Username { get; protected set; }
        public Guid NgoId { get; protected set; }
        public string NgoName { get; protected set; }
        public Guid? ChildId { get; protected set; }
        public string ChildFullName { get; protected set; }
        public decimal Value { get; protected set; }
        public string Type { get; protected set; }
        public string Hash { get; protected set; }
        public DateTime CreatedAt { get; protected set; } 

        protected Donation()
        {
        }

        public Donation(Guid id, User user, Ngo ngo,
            decimal value, string type, string hash) : base(id)
        {
            UserId = user.Id;
            Username = user.Username;
            NgoId = ngo.Id;
            NgoName = ngo.Name;
            Value = value;
            Type = type;
            Hash = hash;
            CreatedAt = DateTime.UtcNow;
        }
        
        public Donation(Guid id, User user, Ngo ngo, Guid childId,
            decimal value, string type, string hash) : base(id)
        {
            UserId = user.Id;
            Username = user.Username;
            NgoId = ngo.Id;
            NgoName = ngo.Name;
            var child = ngo.Children.Single(x => x.Id == childId);
            ChildId = childId;
            ChildFullName = child.FullName;
            Value = value;
            Type = type;
            Hash = hash;
            CreatedAt = DateTime.UtcNow;
        }
    }
}