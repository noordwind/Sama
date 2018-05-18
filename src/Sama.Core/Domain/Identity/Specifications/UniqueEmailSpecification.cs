using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Repositories;

namespace Sama.Core.Domain.Identity.Specifications
{
    public class UniqueEmailSpecification : IUniqueEmailSpecification
    {
        private readonly IUserRepository _userRepository;

        public UniqueEmailSpecification(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(string value)
            => await _userRepository.IsEmailUnique(value);
    }
}