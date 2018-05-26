using System;
using Newtonsoft.Json;

namespace Sama.Services.Ngos.Commands
{
    public class RejectNgo : ICommand
    {
        public Guid NgoId { get; }
        public string Notes { get; }

        [JsonConstructor]
        public RejectNgo(Guid ngoId, string notes)
        {
            NgoId = ngoId;
            Notes = notes;
        }
    }   
}