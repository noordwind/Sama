using System.Threading.Tasks;
using Sama.Core.Domain;

namespace Sama.Services
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}