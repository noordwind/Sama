using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sama.Core.Domain.Identity.Events;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Shared;

namespace Sama.Core.Domain.Identity
{
    public class User : AggregateRoot
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string Role { get; protected set; }
        public string PasswordHash { get; protected set; }
        public decimal Funds { get; protected set; }
        public decimal DonatedFunds { get; protected set; }
        public Wallet Wallet { get; protected set; } = new Wallet(0);
        public IList<Payment> Payments { get; protected set; } = new List<Payment>();
        public IList<Donation> Donations { get; protected set; } = new List<Donation>();
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {
        }

        public User(Guid id, string email, string username, string role) : base(id)
        {
            if (!EmailRegex.IsMatch(email))
            {
                throw new DomainException("invalid_email",
                    $"Invalid email: '{email}'.");
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                throw new DomainException("invalid_username",
                    $"Invalid username: '{username}'.");
            }

            if (!Identity.Role.IsValid(role))
            {
                throw new DomainException("invalid_role",
                    $"Invalid role: '{role}'.");
            }

            Email = email.ToLowerInvariant();
            Username = username.ToLowerInvariant();
            Role = role.ToLowerInvariant();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            AddEvent(new SignedUp(id, email, role));
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddFunds(Payment payment)
        {
            var funds = payment.Value + Wallet.Funds;
            Funds = funds;
            Wallet = new Wallet(funds);
            Payments.Add(payment);
        }

        public Donation DonateChild(Guid id, Ngo ngo, Guid childId, decimal value, string hash)
        {
            Donate(value);
            var donation = new Donation(id, this, ngo, childId, value, "child", hash);
            Donations.Add(donation);

            return donation;
        }

        public Donation DonateNgo(Guid id, Ngo ngo, decimal value, string hash)
        {
            Donate(value);
            var donation = new Donation(id, this, ngo, value, "ngo", hash);
            Donations.Add(donation);

            return donation;
        }

        private void Donate(decimal value)
        {
            var funds = Wallet.Funds;
            if (funds - value < 0)
            {
                throw new DomainException("insufficient_donation_funds",
                    "Insufficient funds for donation.");
            }
            DonatedFunds += value;
            var updatedFunds = funds - value;
            Wallet = new Wallet(updatedFunds);
            Funds = updatedFunds;
        }
    }
}