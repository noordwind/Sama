using System;
using System.Collections.Generic;

namespace Sama.Services.Ngos.Dtos
{
    public class NgoDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal Funds { get; set; }  
        public decimal DonatedFunds { get; set; }
        public bool Approved { get; set; }  
        public IList<ChildInfoDto> Children { get; set; }
        public IList<DonationDto> Donations { get; set; } 
    }
}