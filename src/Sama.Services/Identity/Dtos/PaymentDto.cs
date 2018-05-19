using System;

namespace Sama.Services.Identity.Dtos
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Hash { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}