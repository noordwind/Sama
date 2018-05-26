using System.Threading.Tasks;
using AutoMapper;
using Sama.Services.Ngos.Commands;
using Sama.Services.Shared.Dtos;

namespace Sama.Services.Ngos.Handlers
{
    public class CreateNgoHandler : ICommandHandler<CreateNgo>
    {
        private readonly INgosService _ngosService;
        private readonly IMapper _mapper;

        public CreateNgoHandler(INgosService  ngosService, IMapper mapper)
        {
            _ngosService = ngosService;
            _mapper = mapper;
        }

        public async Task HandleAsync(CreateNgo command)
        {
            await _ngosService.CreateAsync(command.NgoId, command.OwnerId, command.Name,
                _mapper.Map<LocationDto>(command.Location), command.Description, command.FundsPerChild);
        }
    }
}