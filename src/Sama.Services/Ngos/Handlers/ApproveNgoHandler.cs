using System.Threading.Tasks;
using Sama.Services.Ngos.Commands;

namespace Sama.Services.Ngos.Handlers
{
    public class ApproveNgoHandler : ICommandHandler<ApproveNgo>
    {
        private readonly INgosService _ngosService;

        public ApproveNgoHandler(INgosService  ngosService)
        {
            _ngosService = ngosService;
        }
        
        public async Task HandleAsync(ApproveNgo command)
        {
            await _ngosService.ApproveAsync(command.NgoId, command.Notes);
        }
    }
}