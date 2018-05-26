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

        public async Task<IEnumerable<Ngo>> BrowseAsync(string type = "")
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return await _repository.FindAsync(_ => true);
            }
            switch (type.ToLowerInvariant())
            {
                case "new": return await _repository.FindAsync(x => !x.Approved && !x.Rejected);
                case "approved": return await _repository.FindAsync(x => x.Approved);
                case "rejected": return await _repository.FindAsync(x => x.Rejected);
            }
            
            return Enumerable.Empty<Ngo>();
        }

        public async Task CreateAsync(Ngo ngo)
            => await _repository.CreateAsync(ngo);

        public async Task UpdateAsync(Ngo ngo)
            => await _repository.UpdateAsync(ngo);        
    }
}