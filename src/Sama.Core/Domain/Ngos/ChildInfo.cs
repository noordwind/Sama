using System;

namespace Sama.Core.Domain.Ngos
{
    public class ChildInfo : Entity
    {
        public string FullName { get; protected set; }
        public DateTime Birthdate { get; protected set; }
        public string Gender { get; protected set; }
        public string Notes { get; protected set; }
        public decimal GatheredFunds { get; protected set; }
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
            GatheredFunds = child.GatheredFunds;
            NeededFunds = child.NeededFunds;
        }

        public void Donate(decimal value)
        {
            GatheredFunds += value;
        }
    }
}