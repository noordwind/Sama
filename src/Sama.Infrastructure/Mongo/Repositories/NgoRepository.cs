using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Ngos.Repositories;

namespace Sama.Infrastructure.Mongo.Repositories
{
    public class NgoRepository : INgoRepository
    {
        private readonly IMongoRepository<Ngo> _repository;

        public NgoRepository(IMongoRepository<Ngo> repository)
        {
            _repository = repository;
        }

        public async Task<Ngo> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<IEnumerable<Ngo>> BrowseAsync(string state = "")
        {
            if (string.IsNullOrWhiteSpace(state))
            {
                return await _repository.FindAsync(_ => true);
            }
            
            return await _repository.FindAsync(x => x.State == state);
        }

        public async Task CreateAsync(Ngo ngo)
            => await _repository.CreateAsync(ngo);

        public async Task UpdateAsync(Ngo ngo)
            => await _repository.UpdateAsync(ngo);        
    }
}