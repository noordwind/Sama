using System;
using System.Threading.Tasks;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos
{
    public interface IChildService
    {
        Task AddAsync(Guid ngoId, params ChildInfoDto[] children);
        Task EditAsync(Guid ngoId, params ChildInfoDto[] children);
    }
}