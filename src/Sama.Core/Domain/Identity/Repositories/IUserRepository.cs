using System;
using System.Threading.Tasks;

namespace Sama.Core.Domain.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<bool> IsEmailUnique(string email);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
    }
}