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

        protected Child()
        {
        }      

        public Child(Guid id, string fullName, DateTime birthDate) : base(id)
        {
            FullName = fullName;
            Birthdate = birthDate;
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