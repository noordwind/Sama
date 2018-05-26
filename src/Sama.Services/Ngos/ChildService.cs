using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Ngos.Repositories;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos
{
    public class ChildService : IChildService
    {
        private readonly INgoRepository _ngoRepository;

        public ChildService(INgoRepository ngoRepository)
        {
            _ngoRepository = ngoRepository;
        }

        public async Task AddAsync(Guid ngoId, params ChildInfoDto[] children)
            => await ExecuteAsync(ngoId, children, (n,c) => n.AddChildren(c));

        public async Task EditAsync(Guid ngoId, params ChildInfoDto[] children)
            => await ExecuteAsync(ngoId, children, (n,c) => n.EditChildren(c));

        private async Task ExecuteAsync(Guid ngoId, IList<ChildInfoDto> children, 
            Action<Ngo,IEnumerable<Child>> action)
        {
            if (children == null || !children.Any())
            {
                throw new ServiceException("children_not_provided", 
                    $"Children were not provided for NGO with id: '{ngoId}'.");
            }
            var ngo = await _ngoRepository.GetAsync(ngoId);
            if (ngo == null)
            {
                throw new ServiceException("ngo_not_found", 
                    $"NGO with id: '{ngoId}' was not found.");
            }
            var ngoChildren = children.Select(x => new Child(x.Id, x.FullName, x.Gender,
                x.Birthdate, x.Notes, x.NeededFunds > 0 ? x.NeededFunds : ngo.FundsPerChild));
            action(ngo, ngoChildren);
            await _ngoRepository.UpdateAsync(ngo);
        }
    }
}