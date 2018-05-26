using Sama.Services.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sama.Services.Ngos;
using System;
using Sama.Api.Framework;
using Sama.Services.Ngos.Commands;
using Sama.Infrastructure.Mvc;
using Sama.Services.Ngos.Queries;

namespace Sama.Api.Controllers
{
    [Route("ngos/{ngoId}/children")]
    public class ChildrenController : BaseController
    {

        public ChildrenController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost]
        [Ngo]
        public async Task<IActionResult> Post(Guid ngoId, [FromBody] AddChildren command)
            => await DispatchAsync(command.Bind(c => c.NgoId, ngoId));
        
        [HttpPut]
        [Ngo]
        public async Task<IActionResult> Put(Guid ngoId, [FromBody] EditChildren command)
            => await DispatchAsync(command.Bind(c => c.NgoId, ngoId));
    }
}