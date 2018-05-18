using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Events;

namespace Sama.Services.Identity.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        public async Task HandleAsync(SignedUp @event)
        {
            await Task.CompletedTask;
        }
    }
}