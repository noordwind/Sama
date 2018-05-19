using System.Threading.Tasks;
using Sama.Services.Ngos.Commands;
using Sama.Services.Shared.Services;

namespace Sama.Services.Ngos.Handlers
{
    public class DonateNgoHandler : ICommandHandler<DonateNgo>
    {
        private readonly IDonationsService _donationsService;

        public DonateNgoHandler(IDonationsService donationsService)
        {
            _donationsService = donationsService;
        }

        public async Task HandleAsync(DonateNgo command)
            => await _donationsService.DonateAsync(command.NgoId, command.UserId, command.Funds);
    }
}