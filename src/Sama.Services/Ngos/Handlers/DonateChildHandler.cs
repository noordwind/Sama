using System.Threading.Tasks;
using Sama.Services.Ngos.Commands;
using Sama.Services.Shared.Services;

namespace Sama.Services.Ngos.Handlers
{
    public class DonateChildHandler : ICommandHandler<DonateChild>
    {
        private readonly IDonationsService _donationsService;

        public DonateChildHandler(IDonationsService donationsService)
        {
            _donationsService = donationsService;
        }

        public async Task HandleAsync(DonateChild command)
            => await _donationsService.DonateChildAsync(command.NgoId, command.ChildId,
                command.UserId, command.Funds);
    }
}