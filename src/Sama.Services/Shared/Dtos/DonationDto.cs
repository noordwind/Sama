using System;

namespace Sama.Services.Shared.Dtos
{
    public class DonationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid NgoId { get; set; }
        public string NgoName { get; set; }
        public decimal Value { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt { get; set; }         
    }
}