using System;
using System.Threading.Tasks;
using Sama.Infrastructure.Authentication;
using Sama.Services.Identity.Dtos;

namespace Sama.Services.Identity
{
    public interface IIdentityService
    {
        Task<UserDto> GetAsync(Guid id);
        Task AddFunds(Guid id, decimal funds);
        Task SignUpAsync(Guid id, string email, string username, string password, string role = "user");
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);         
    }
}