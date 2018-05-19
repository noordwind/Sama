using System;
using System.Collections.Generic;
using System.Linq;

namespace Sama.Core.Domain.Ngos
{
    public class Ngo : AggregateRoot
    {
        public Guid OwnerId { get; protected set; }
        public string Name { get; protected set; }
        public string Address { get; protected set; }
        public string Description { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }
        public decimal Funds { get; protected set; }
        public decimal DonatedFunds { get; protected set; }
        public bool Approved { get; protected set; }  
        public IList<ChildInfo> Children { get; protected set; } = new List<ChildInfo>();
        public IList<Donation> Donations { get; protected set; } = new List<Donation>();

        protected Ngo()
        {
        }      

        public Ngo(Guid id, Guid ownerId, string name, string address,
            double latitude, double longitude, bool approved = true) : base(id)
        {
            OwnerId = ownerId;
            Name = name;
            Description = $"{Name} description.";
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
            Approved = approved;
        }

        public void Approve()
        {
            Approved = true;
        }

        public void Donate(Donation donation)
        {
            Donations.Add(donation);
            Funds += donation.Value;
        }

        public void DonateChildren()
        {
            if (Children.Count == 0 || Funds == 0)
            {
                return;
            }
            if (Funds <= 0)
            {
                throw new DomainException("insufficient_ngo_funds", "Insufficient NGO funds for children donation.");                
            }
            var splitFunds = Funds / Children.Count;
            foreach (var child in Children)
            {
                child.Donate(splitFunds);
                DonatedFunds += splitFunds;
                Funds -= splitFunds;
            }
        }

        public void AddChild(Child child)
        {
            if (Children.Any(x => x.Id == child.Id))
            {
                return;
            }
            Children.Add(new ChildInfo(child));
        }
    }
}