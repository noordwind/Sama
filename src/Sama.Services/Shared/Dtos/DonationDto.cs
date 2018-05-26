using System;

namespace Sama.Services.Shared.Dtos
{
    public class DonationDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid NgoId { get; set; }
        public string NgoName { get; set; }
        public Guid? ChildId { get; set; }
        public string ChildFullName { get; set; }
        public decimal Value { get; set; }
        public string Type { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt { get; set; }         
    }
}