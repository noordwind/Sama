using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Repositories;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Ngos.Repositories;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos
{
    public class NgosService : INgosService
    {
        private readonly INgoRepository _ngoRepository;
        private readonly IUserRepository _userRepository;

        public NgosService(INgoRepository ngoRepository,
            IUserRepository userRepository)
        {
            _ngoRepository = ngoRepository;
            _userRepository = userRepository;
        }

        public async Task<NgoDto> GetAsync(Guid id)
        {
            var ngo = await _ngoRepository.GetAsync(id);
            
            return ngo == null ? null : Map(ngo);
        }

        public async Task<IEnumerable<NgoDto>> GetAllAsync()
        {
            var ngos = await _ngoRepository.GetAllAsync();

            return ngos.Select(Map);
        }

        private NgoDto Map(Ngo ngo)
            => new NgoDto
                {
                    Id = ngo.Id,
                    OwnerId = ngo.OwnerId,
                    Name = ngo.Name,
                    Address = ngo.Address,
                    Description = ngo.Description,
                    Latitude = ngo.Latitude,
                    Longitude = ngo.Longitude,
                    Funds = ngo.Funds,
                    DonatedFunds = ngo.DonatedFunds,
                    Approved = ngo.Approved,
                    Children = ngo.Children.Select(x => 
                        new ChildInfoDto
                        {
                            Id = x.Id,
                            FullName = x.FullName,
                            Birthdate = x.Birthdate,
                            Funds = x.Funds,
                            NeededFunds = x.NeededFunds
                        }).ToList(),
                    Donations = ngo.Donations.Select(x => 
                        new DonationDto
                        {
                            Id = x.Id,
                            UserId = x.UserId,
                            Value = x.Value,
                            Hash = x.Hash,
                            CreatedAt = x.CreatedAt
                        }).ToList()
                };

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
            ngo.Donate(new Donation(donation.Id, userId, donation.Value, donation.Hash));
            ngo.DonateChildren();
            await _userRepository.UpdateAsync(user);
            await _ngoRepository.UpdateAsync(ngo);
        }
    }
}