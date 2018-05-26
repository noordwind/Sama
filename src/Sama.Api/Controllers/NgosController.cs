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
    public class NgosController : BaseController
    {
        private readonly INgosService _ngosService;

        public NgosController(ICommandDispatcher commandDispatcher,
            INgosService ngosService) : base(commandDispatcher)
        {
            _ngosService = ngosService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] BrowseNgos query)
        {
            var ngos = await _ngosService.BrowseAsync(query);

            return Ok(ngos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            var ngo = await _ngosService.GetAsync(id);
            
            return Single(ngo);
        }

        [HttpPost]
        [Ngo]
        public async Task<IActionResult> Create([FromBody] CreateNgo command)
            => await DispatchAsync(command.BindId(c => c.NgoId).Bind(c => c.OwnerId, UserId),
                createdAt: nameof(Get), routeValues: new {id = command.NgoId}, resourceId: command.NgoId );
        
        [HttpPost("{ngoId}/approve")]
        [Admin]
        public async Task<IActionResult> Approve(Guid ngoId, [FromBody] ApproveNgo command)
            => await DispatchAsync(command.Bind(c => c.NgoId, ngoId));
        
        [HttpPost("{ngoId}/reject")]
        [Admin]
        public async Task<IActionResult> Reject(Guid ngoId, [FromBody] RejectNgo command)
            => await DispatchAsync(command.Bind(c => c.NgoId, ngoId));
        
        [HttpPost("{ngoId}/donate")]
        public async Task<IActionResult> Donate(Guid ngoId, [FromBody] DonateNgo command)
            => await DispatchAsync(command.Bind(c => c.NgoId, ngoId).Bind(c => c.UserId, UserId));
    }
}