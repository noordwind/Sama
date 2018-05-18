using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Ngos.Repositories;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos
{
    public class NgosService : INgosService
    {
        private readonly INgoRepository _ngoRepository;

        public NgosService(INgoRepository ngoRepository)
        {
            _ngoRepository = ngoRepository;
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
                    Approved = ngo.Approved,
                    Children = ngo.Children.Select(c => 
                    new ChildInfoDto
                    {
                        Id = c.Id,
                        FullName = c.FullName,
                        Birthdate = c.Birthdate,
                        Funds = c.Funds
                    }).ToList()
                };
    }
}