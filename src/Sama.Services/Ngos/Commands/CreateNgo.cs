using System;
using Sama.Services.Shared.Commands.Models;

namespace Sama.Services.Ngos.Commands
{
    public class CreateNgo : ICommand
    {
        public Guid NgoId { get; }
        public Guid OwnerId { get; }
        public string Name { get; }
        public Location Location { get; }
        public string Description { get; }
        public decimal FundsPerChild { get; }

        public CreateNgo(Guid ngoId, Guid ownerId, string name, 
            Location location, string description, decimal fundsPerChild)
        {
            NgoId = ngoId;
            OwnerId = ownerId;
            Name = name;
            Location = location;
            Description  = description;
            FundsPerChild = fundsPerChild;
        }
    }
}