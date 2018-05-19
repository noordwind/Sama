using System;

namespace Sama.Services.Ngos.Dtos
{
    public class ChildInfoDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public decimal Funds { get; set; } 
        public decimal NeededFunds { get; set; }
    }
}