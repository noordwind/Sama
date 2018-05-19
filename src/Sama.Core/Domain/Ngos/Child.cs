using System;

namespace Sama.Core.Domain.Ngos
{
    public class Child : AggregateRoot
    {
        public Guid NgoId { get; protected set; }
        public string NgoName { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime Birthdate { get; protected set; }
        public decimal Funds { get; protected set; }
        public decimal NeededFunds { get; protected set; }

        protected Child()
        {
        }      

        public Child(Guid id, string fullName, 
            DateTime birthDate, decimal neededFunds = 500) : base(id)
        {
            FullName = fullName;
            Birthdate = birthDate;
            NeededFunds = neededFunds;
        }

        public void Donate(decimal value)
        {
            Funds += value;
        }

        public void SetNgo(Ngo ngo)
        {
            NgoId = ngo.Id;
            NgoName = ngo.Name;
        }
    }
}