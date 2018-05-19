using System;
using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Repositories;
using Sama.Core.Domain.Identity.Services;
using Sama.Core.Domain.Identity.Specifications;

namespace Sama.Core.Domain.Identity.Factories
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly IUniqueEmailSpecification _uniqueEmailSpecification;
        private readonly IPasswordHasher _passwordHasher;

        public UserFactory(IUserRepository userRepository,
            IUniqueEmailSpecification uniqueEmailSpecification,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _uniqueEmailSpecification = uniqueEmailSpecification;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> CreateAsync(Guid id, string email, string username, string password, string role = Role.User)
        {
            var isEmailUnique = await _uniqueEmailSpecification.IsSatisfiedByAsync(email);
            if (!isEmailUnique)
            {
                throw new DomainException("email_in_use",
                    $"Email: '{email}' is already in use.");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                role = Role.User;
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                username = $"user-{id}";
            }
            var user = new User(id, email, username, role);
            _passwordHasher.SetPasswordHash(user, password);

            return user;
        }
    }
}