using System;

namespace Sama.Core.Domain.Ngos
{
    public class ChildInfo : Entity
    {
        public string FullName { get; protected set; }
        public DateTime Birthdate { get; protected set; }
        public string Gender { get; protected set; }
        public string Notes { get; protected set; }
        public decimal Funds { get; protected set; }
        public decimal NeededFunds { get; protected set; }

        protected ChildInfo()
        {
        }

        public ChildInfo(Child child)
        {
            Id = child.Id;
            FullName = child.FullName;
            Birthdate = child.Birthdate;
            Gender = child.Gender;
            Notes = child.Notes;
            Funds = child.GatheredFunds;
            NeededFunds = child.NeededFunds;
        }

        public void Donate(decimal value)
        {
            Funds += value;
        }
    }
}