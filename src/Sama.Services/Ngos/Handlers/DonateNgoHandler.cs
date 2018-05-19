using System.Threading.Tasks;
using Sama.Services.Ngos.Commands;

namespace Sama.Services.Ngos.Handlers
{
    public class DonateNgoHandler : ICommandHandler<DonateNgo>
    {
        private readonly INgosService _ngosService;

        public DonateNgoHandler(INgosService ngosService)
        {
            _ngosService = ngosService;
        }

        public async Task HandleAsync(DonateNgo command)
            => await _ngosService.DonateAsync(command.NgoId, command.UserId, command.Funds);
    }
}