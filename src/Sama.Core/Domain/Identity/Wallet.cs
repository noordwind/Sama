namespace Sama.Core.Domain.Identity
{
    public class Wallet : IValueObject
    {
        public decimal Funds { get; protected set; }

        protected Wallet()
        {
        }

        public Wallet(decimal funds)
        {
            Funds = funds;
        }
    }
}