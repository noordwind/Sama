using System.Threading.Tasks;
using Sama.Core.Domain.Identity.Events;

namespace Sama.Services.Identity.Handlers
{
    public class SignedUpHandler : IEventHandler<SignedUp>
    {
        private readonly IIdentityService _identityService;

        public SignedUpHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task HandleAsync(SignedUp @event)
        {
            await _identityService.AddFunds(@event.UserId, 30000);
        }
    }
}