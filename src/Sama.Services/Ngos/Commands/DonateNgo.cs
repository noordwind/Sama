using System;
using Newtonsoft.Json;

namespace Sama.Services.Ngos.Commands
{
    public class DonateNgo : ICommand
    {
        public Guid NgoId { get; }
        public Guid UserId { get; set; }
        public decimal Funds { get; }

        [JsonConstructor]
        public DonateNgo(Guid ngoId, Guid userId, decimal funds)
        {
            NgoId = ngoId;
            UserId = userId;
            Funds = funds;
        }
    }        
}