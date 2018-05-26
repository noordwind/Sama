using System;
using Newtonsoft.Json;

namespace Sama.Services.Ngos.Commands.Models
{
    public class Child
    {
        public Guid Id { get; }
        public string FullName { get; }
        public string Gender { get; }
        public DateTime Birthdate { get; }
        public string Notes { get; }
        public decimal? NeededFunds { get; }

        [JsonConstructor]
        public Child(Guid id, string fullName, string gender,
            DateTime birthdate, string notes, decimal? neededFunds)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            FullName = fullName;
            Gender = gender;
            Birthdate = birthdate;
            Notes = notes;
            NeededFunds = neededFunds;
        }
    }
}