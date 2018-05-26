using System.Threading.Tasks;

namespace Sama.Infrastructure.Maps
{
    public interface ILocationProvider
    {
        Task<Location> GetAsync(string address);
        Task<Location> GetAsync(double latitude, double longitude);        
    }
}