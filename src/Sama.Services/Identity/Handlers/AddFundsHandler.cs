using System.Threading.Tasks;
using Sama.Services.Identity.Commands;

namespace Sama.Services.Identity.Handlers
{
    public class AddFundsHandler : ICommandHandler<AddFunds>
    {
        private readonly IIdentityService _identityService;

        public AddFundsHandler(IIdentityService identityService)
        {
            _identityService = identityService;            
        }

        public async Task HandleAsync(AddFunds command)
            => await _identityService.AddFunds(command.UserId, command.Funds);
    }
}