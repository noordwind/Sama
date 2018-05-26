using System;
using System.Collections.Generic;
using System.Linq;
using Sama.Core.Domain.Shared;

namespace Sama.Core.Domain.Ngos
{
    public class Ngo : AggregateRoot
    {
        public Guid OwnerId { get; protected set; }
        public string Name { get; protected set; }
        public Location Location { get; protected set; }
        public string Description { get; protected set; }
        public decimal AvailableFunds { get; protected set; }
        public decimal DonatedFunds { get; protected set; }
        public decimal FundsPerChild { get; protected set; }
        public string State { get; protected set; } 
        public string Notes { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IList<ChildInfo> Children { get; protected set; } = new List<ChildInfo>();
        public IList<Donation> Donations { get; protected set; } = new List<Donation>();

        protected Ngo()
        {
        }      

        public Ngo(Guid id, Guid ownerId, string name, Location location,  
            string description, decimal fundsPerChild = 1000, bool approved = false) : base(id)
        {
            if (fundsPerChild <= 0)
            {
                throw new DomainException("invalid_funds_per_child",
                    $"Invalid funds per child: {fundsPerChild}.");
            }
            OwnerId = ownerId;
            Name = name;
            Location = location;
            Description = description;
            FundsPerChild = fundsPerChild;
            State = approved ? "approved" : "new";
            Notes = string.Empty;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Approve(string notes)
        {
            State = "approved";
            Notes = notes ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reject(string notes)
        {
            State = "rejected";
            Notes = notes ?? string.Empty;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DonateChild(Donation donation)
        {
            Donate(donation);
            var child = Children.SingleOrDefault(x => x.Id == donation.ChildId);
            if (child == null)
            {
                throw new DomainException("child_not_found", 
                    $"Child with id: '{donation.ChildId}' was not found.");
            }
            child.Donate(donation.Value);
        }

        public void DonateChildren(Donation donation)
        {
            Donate(donation);
            if (Children.Count == 0 || AvailableFunds == 0)
            {
                return;
            }
            if (Children.All(x => x.GatheredFunds >= x.NeededFunds))
            {
                return;
            }
            if (AvailableFunds <= 0)
            {
                throw new DomainException("insufficient_ngo_funds", "Insufficient NGO funds for children donations.");                
            }
            foreach (var child in Children.Where(x => x.GatheredFunds < x.NeededFunds).OrderByDescending(x => x.NeededFunds))
            {
                var neededFunds = child.NeededFunds - child.GatheredFunds;
                if (AvailableFunds - neededFunds < 0)
                {
                    neededFunds = AvailableFunds;
                }
                if (neededFunds < 0)
                {
                    return;
                }
                child.Donate(neededFunds);
                DonatedFunds += neededFunds;
                AvailableFunds -= neededFunds;
            }
        }
        
        private void Donate(Donation donation)
        {
            Donations.Add(donation);
            AvailableFunds += donation.Value;
        }

        public void AddChildren(IEnumerable<Child> children)
        {
            foreach (var child in children)
            {
                AddChild(child);
            }
        }

        public void AddChild(Child child)
        {
            if (Children.Any(x => x.Id == child.Id))
            {
                return;
            }
            if (Children.Any(x =>
                string.Compare(x.FullName, child.FullName, StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                return;
            }
            Children.Add(new ChildInfo(child));
            UpdatedAt = DateTime.UtcNow;
        }
        
        public void EditChildren(IEnumerable<Child> children)
        {
            foreach (var child in children)
            {
                EditChild(child);
            }
        }

        public void EditChild(Child child)
        {
            var existingChild = Children.FirstOrDefault(x => x.Id == child.Id);
            if (existingChild == null)
            {
                return;
            }
            if (Children.Any(x => x.Id != child.Id &&
                string.Compare(x.FullName, child.FullName, StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                return;
            }
            Children.Remove(existingChild);
            Children.Add(new ChildInfo(child));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}