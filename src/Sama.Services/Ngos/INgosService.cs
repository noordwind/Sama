using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sama.Services.Ngos.Dtos;
using Sama.Services.Ngos.Queries;
using Sama.Services.Shared.Dtos;

namespace Sama.Services.Ngos
{
    public interface INgosService
    {
        Task<NgoDto> GetAsync(Guid id);
        Task<IEnumerable<NgoDto>> BrowseAsync(BrowseNgos query);

        Task CreateAsync(Guid id, Guid ownerId, string name, 
            LocationDto location, string description, decimal fundsPerChild);

        Task ApproveAsync(Guid ngoId, string notes);
        Task RejectAsync(Guid ngoId, string notes);
    }
}