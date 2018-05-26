using System;

namespace Sama.Core.Domain.Ngos
{
    public class Child : AggregateRoot
    {
        public Guid NgoId { get; protected set; }
        public string NgoName { get; protected set; }
        public string FullName { get; protected set; }
        public string Gender { get; protected set; }
        public DateTime Birthdate { get; protected set; }
        public string Notes { get; protected set; }
        public decimal GatheredFunds { get; protected set; }
        public decimal NeededFunds { get; protected set; }

        protected Child()
        {
        }      

        public Child(Guid id, string fullName, 
            string gender, DateTime birthDate, 
            string notes = "", decimal neededFunds = 1000) : base(id)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new DomainException("invalid_fullname",
                    $"Invalid full name: '{fullName}'.");
            }
            if (gender != "male" && gender != "female")
            {
                throw new DomainException("invalid_gender",
                    $"Invalid gender: '{gender}'.");
            }
            if (neededFunds <= 0)
            {
                throw new DomainException("invalid_needed_funds",
                    $"Invalid needed funds: '{neededFunds}'.");                
            }
            Id = id;
            FullName = fullName;
            Gender = gender;
            Birthdate = birthDate;
            Notes = notes ?? string.Empty;;
            NeededFunds = neededFunds;
        }

        public void Donate(decimal funds)
        {
            GatheredFunds += funds;
        }

        public void SetNgo(Ngo ngo)
        {
            NgoId = ngo.Id;
            NgoName = ngo.Name;
        }
    }
}