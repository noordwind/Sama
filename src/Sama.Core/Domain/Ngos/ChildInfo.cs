using System;

namespace Sama.Core.Domain.Ngos
{
    public class ChildInfo : Entity
    {
        public string FullName { get; protected set; }
        public DateTime Birthdate { get; protected set; }
        public decimal Funds { get; protected set; }  

        protected ChildInfo()
        {
        }

        public ChildInfo(Child child)
        {
            Id = child.Id;
            FullName = child.FullName;
            Birthdate = child.Birthdate;
            Funds = child.Funds;
        }

        public void Donate(decimal value)
        {
            Funds += value;
        }
    }
}