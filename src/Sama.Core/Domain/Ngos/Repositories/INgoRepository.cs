using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sama.Core.Domain.Ngos.Repositories
{
    public interface INgoRepository
    {
        Task<Ngo> GetAsync(Guid id);
        Task<IEnumerable<Ngo>> BrowseAsync(string type = "");
        Task CreateAsync(Ngo ngo);
        Task UpdateAsync(Ngo ngo);  
    }
}