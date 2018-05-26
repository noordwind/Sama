using System;
using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Repositories;
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

        public async Task DonateAsync(Guid ngoId, Guid userId, decimal value)
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
            var donation = user.Donate(Guid.NewGuid(), ngo.Id, ngo.Name, value, "hash");
            ngo.Donate(donation);
            ngo.DistributeFundsToChildren();
            await _userRepository.UpdateAsync(user);
            await _ngoRepository.UpdateAsync(ngo);
        }
    }
}