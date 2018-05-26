using System.Threading.Tasks;
using Sama.Services.Ngos.Commands;

namespace Sama.Services.Ngos.Handlers
{
    public class RejectNgoHandler : ICommandHandler<RejectNgo>
    {
        private readonly INgosService _ngosService;

        public RejectNgoHandler(INgosService  ngosService)
        {
            _ngosService = ngosService;
        }
        
        public async Task HandleAsync(RejectNgo command)
        {
            await _ngosService.RejectAsync(command.NgoId, command.Notes);
        }
    }
}