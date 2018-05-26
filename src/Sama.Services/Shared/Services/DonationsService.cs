using System;
using System.Threading.Tasks;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Repositories;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Ngos.Repositories;

namespace Sama.Services.Shared.Services
{
    public class DonationsService : IDonationsService
    {
        private readonly INgoRepository _ngoRepository;
        private readonly IUserRepository _userRepository;

        public DonationsService(INgoRepository ngoRepository, IUserRepository userRepository)
        {
            _ngoRepository = ngoRepository;
            _userRepository = userRepository;
        }

        public async Task DonateNgoAsync(Guid ngoId, Guid userId, decimal value)
        {
            var ngoAndUser = await GetNgoAndUserAsync(ngoId, userId);
            var ngo = ngoAndUser.Item1;
            var user = ngoAndUser.Item2;
            var donation = user.DonateNgo(Guid.NewGuid(), ngo, value, "hash");
            ngo.DonateChildren(donation);
            await _userRepository.UpdateAsync(user);
            await _ngoRepository.UpdateAsync(ngo);
        }

        public async Task DonateChildAsync(Guid ngoId, Guid childId, Guid userId, decimal value)
        {
            var ngoAndUser = await GetNgoAndUserAsync(ngoId, userId);
            var ngo = ngoAndUser.Item1;
            var user = ngoAndUser.Item2;
            var donation = user.DonateChild(Guid.NewGuid(), ngo, childId, value, "hash");
            ngo.DonateChild(donation);
            await _userRepository.UpdateAsync(user);
            await _ngoRepository.UpdateAsync(ngo);
        }

        private async Task<(Ngo, User)> GetNgoAndUserAsync(Guid ngoId, Guid userId)
        {
            var ngo = await _ngoRepository.GetAsync(ngoId);
            if (ngo == null)
            {
                throw new ServiceException("ngo_not_found", $"NGO with id: '{ngoId}' was not found.");
            }
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException("user_not_found", $"User with id: '{userId}'  was not found.");
            }

            return (ngo, user);
        }
    }
}