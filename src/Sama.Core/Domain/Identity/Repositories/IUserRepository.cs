using System;
using System.Threading.Tasks;

namespace Sama.Core.Domain.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUsernameAsync(string username);
        Task<bool> IsEmailUnique(string email);
        Task<bool> IsUsernamelUnique(string username);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
    }
}