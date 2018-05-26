using System;

namespace Sama.Services.Ngos.Dtos
{
    public class ChildInfoDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Notes { get; set; }
        public decimal GatheredFunds { get; set; } 
        public decimal NeededFunds { get; set; }
    }
}