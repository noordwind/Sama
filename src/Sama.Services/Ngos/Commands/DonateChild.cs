using System;
using Newtonsoft.Json;

namespace Sama.Services.Ngos.Commands
{
    public class DonateChild : ICommand
    {
        public Guid NgoId { get; }
        public Guid ChildId { get; }
        public Guid UserId { get; }
        public decimal Funds { get; }

        [JsonConstructor]
        public DonateChild(Guid ngoId, Guid childId, Guid userId, decimal funds)
        {
            NgoId = ngoId;
            ChildId = childId;
            UserId = userId;
            Funds = funds;
        }
    }   
}