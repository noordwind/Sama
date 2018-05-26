using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Sama.Core.Domain.Identity.Repositories;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Ngos.Repositories;
using Sama.Infrastructure.Maps;
using Sama.Services.Ngos.Dtos;
using Sama.Services.Ngos.Queries;
using Sama.Services.Shared.Dtos;

namespace Sama.Services.Ngos
{
    public class NgosService : INgosService
    {
        private readonly INgoRepository _ngoRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILocationProvider _locationProvider;
        private readonly IMapper _mapper;

        public NgosService(INgoRepository ngoRepository,
            IUserRepository userRepository,
            ILocationProvider locationProvider,
            IMapper mapper)
        {
            _ngoRepository = ngoRepository;
            _userRepository = userRepository;
            _locationProvider = locationProvider;
            _mapper = mapper;
        }

        public async Task<NgoDto> GetAsync(Guid id)
            => _mapper.Map<NgoDto>(await _ngoRepository.GetAsync(id));

        public async Task<IEnumerable<NgoDto>> BrowseAsync(BrowseNgos query)
            => await _ngoRepository.BrowseAsync(query.State)
                .ContinueWith(c => c.Result.Select(_mapper.Map<NgoDto>));

        public async Task CreateAsync(Guid id, Guid ownerId, string name,
            LocationDto location, string description, decimal fundsPerChild)
        {
            if (location == null)
            {
                throw new ServiceException("invalid_location", "Invalid location.");
            }

            Location foundLocation = null;
            if (!string.IsNullOrWhiteSpace(location.Address))
            {
                foundLocation = await _locationProvider.GetAsync(location.Address);
            }
            if (foundLocation == null && (Math.Abs(location.Latitude) > 0.01 && Math.Abs(location.Longitude) > 0.01))
            {
                foundLocation = await _locationProvider.GetAsync(location.Latitude, location.Longitude);
            }
            if (foundLocation == null)
            {
                throw new ServiceException("address_not_found",
                    $"Address: '{location.Address} [lat: {location.Latitude}, " +
                    $"lng: {location.Longitude}]' was not found.");
            }

            var ngo = new Ngo(id, ownerId, name, new Core.Domain.Shared.Location(foundLocation.Address,
                foundLocation.Latitude, foundLocation.Longitude), description, fundsPerChild);
            await _ngoRepository.CreateAsync(ngo);
        }

        public async Task ApproveAsync(Guid id, string notes)
        {
            var ngo = await _ngoRepository.GetAsync(id);
            if (ngo == null)
            {
                throw new ServiceException("ngo_not_found",
                    $"NGO with id: '{id}' was not found.");
            }

            ngo.Approve(notes);
            await _ngoRepository.UpdateAsync(ngo);
        }

        public async Task RejectAsync(Guid id, string notes)
        {
            var ngo = await _ngoRepository.GetAsync(id);
            if (ngo == null)
            {
                throw new ServiceException("ngo_not_found",
                    $"NGO with id: '{id}' was not found.");
            }

            ngo.Reject(notes);
            await _ngoRepository.UpdateAsync(ngo);
        }
    }
}