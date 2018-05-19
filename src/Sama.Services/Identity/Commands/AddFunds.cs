using System;
using Newtonsoft.Json;

namespace Sama.Services.Identity.Commands
{
    public class AddFunds : ICommand
    {
        public Guid UserId { get; set; }
        public decimal Funds { get; }

        [JsonConstructor]
        public AddFunds(Guid userId, decimal funds)
        {
            UserId = userId;
            Funds = funds;
        }
    }    
}