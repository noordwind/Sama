using System;
using System.Threading.Tasks;

namespace Sama.Services.Shared.Services
{
    public interface IDonationsService
    {
        Task DonateAsync(Guid ngoId, Guid userId, decimal value);
    }
}