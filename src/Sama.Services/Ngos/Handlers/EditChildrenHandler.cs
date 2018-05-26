using System.Threading.Tasks;
using AutoMapper;
using Sama.Services.Ngos.Commands;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos.Handlers
{
    public class EditChildrenHandler : ICommandHandler<EditChildren>
    {
        private readonly IChildService _childService;
        private readonly IMapper _mapper;

        public EditChildrenHandler(IChildService childService, IMapper mapper)
        {
            _childService = childService;
            _mapper = mapper;
        }

        public async Task HandleAsync(EditChildren command)
        {
            await _childService.EditAsync(command.NgoId, _mapper.Map<ChildInfoDto[]>(command.Children));
        }
    }
}