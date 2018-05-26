using System;
using System.Threading.Tasks;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Repositories;

namespace Sama.Infrastructure.Mongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;

        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<User> GetByEmailAsync(string email)
            => await _repository.GetAsync(x => x.Email == email.ToLowerInvariant());

        public async Task<User> GetByUsernameAsync(string username)
            => await _repository.GetAsync(x => x.Username == username.ToLowerInvariant());

        public async Task<bool> IsEmailUnique(string email)
            => await _repository.ExistsAsync(x => x.Email == email.ToLowerInvariant()) == false;

        public async Task<bool> IsUsernamelUnique(string username)
            => await _repository.ExistsAsync(x => x.Username == username.ToLowerInvariant()) == false;

        public async Task CreateAsync(User user)
            => await _repository.CreateAsync(user);

        public async Task UpdateAsync(User user)
            => await _repository.UpdateAsync(user);
    }
}