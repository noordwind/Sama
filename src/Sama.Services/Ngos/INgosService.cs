using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos
{
    public interface INgosService
    {
        Task<NgoDto> GetAsync(Guid id);
        Task<IEnumerable<NgoDto>> GetAllAsync();
        Task DonateAsync(Guid ngoId, Guid userId, decimal value);
    }
}