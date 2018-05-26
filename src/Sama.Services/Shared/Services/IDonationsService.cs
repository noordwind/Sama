using System;
using System.Threading.Tasks;

namespace Sama.Services.Shared.Services
{
    public interface IDonationsService
    {
        Task DonateNgoAsync(Guid ngoId, Guid userId, decimal value);
        Task DonateChildAsync(Guid ngoId, Guid childId, Guid userId, decimal value);
    }
}