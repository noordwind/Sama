using System;
using System.Threading.Tasks;

namespace Sama.Core.Domain.Identity.Factories
{
    public interface IUserFactory
    {
        Task<User> CreateAsync(Guid id, string email, string username, string password, string role = Role.User);
    }
}