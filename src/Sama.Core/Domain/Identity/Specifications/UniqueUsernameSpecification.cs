using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Repositories;

namespace Sama.Core.Domain.Identity.Specifications
{
    public class UniqueUsernameSpecification : IUniqueUsernameSpecification
    {
        private readonly IUserRepository _userRepository;

        public UniqueUsernameSpecification(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(string value)
            => await _userRepository.IsUsernamelUnique(value);
    }
}