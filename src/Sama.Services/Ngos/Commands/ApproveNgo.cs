using System;
using Newtonsoft.Json;

namespace Sama.Services.Ngos.Commands
{
    public class ApproveNgo : ICommand
    {
        public Guid NgoId { get; }
        public string Notes { get; }

        [JsonConstructor]
        public ApproveNgo(Guid ngoId, string notes)
        {
            NgoId = ngoId;
            Notes = notes;
        }
    } 
}