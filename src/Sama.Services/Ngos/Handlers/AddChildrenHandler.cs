using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Sama.Services.Ngos.Commands;
using Sama.Services.Ngos.Dtos;

namespace Sama.Services.Ngos.Handlers
{
    public class AddChildrenHandler : ICommandHandler<AddChildren>
    {
        private readonly IChildService _childService;
        private readonly IMapper _mapper;

        public AddChildrenHandler(IChildService childService, IMapper mapper)
        {
            _childService = childService;
            _mapper = mapper;
        }

        public async Task HandleAsync(AddChildren command)
        {
            await _childService.AddAsync(command.NgoId, _mapper.Map<ChildInfoDto[]>(command.Children));
        }
    }
}