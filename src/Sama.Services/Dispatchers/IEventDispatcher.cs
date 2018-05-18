using System.Threading.Tasks;
using Sama.Core.Domain;

namespace Sama.Services.Dispatchers
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(params IEvent[] events);
    }
}