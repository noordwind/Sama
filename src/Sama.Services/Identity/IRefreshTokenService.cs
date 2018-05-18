using System;
using System.Threading.Tasks;
using Sama.Infrastructure.Authentication;

namespace Sama.Services.Identity
{
    public interface IRefreshTokenService
    {
        Task CreateAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);
    }
}