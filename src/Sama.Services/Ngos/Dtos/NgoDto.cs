using System;
using System.Collections.Generic;
using Sama.Services.Shared.Dtos;

namespace Sama.Services.Ngos.Dtos
{
    public class NgoDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LocationDto Location { get; set; }
        public decimal AvailableFunds { get; set; }
        public decimal DonatedFunds { get; set; }
        public decimal FundsPerChild { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IList<ChildInfoDto> Children { get; set; }
        public IList<DonationDto> Donations { get; set; } 
    }
}