using System;
using System.Collections.Generic;
using Sama.Services.Shared.Dtos;

namespace Sama.Services.Identity.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public decimal Funds { get; set; }
        public decimal DonatedFunds { get; set; }
        public DateTime CreatedAt { get; set; }
        public WalletDto Wallet { get; set; }
        public IList<PaymentDto> Payments { get; set; }
        public IList<DonationDto> Donations { get; set; }
    }
}