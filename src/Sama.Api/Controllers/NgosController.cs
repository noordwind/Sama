using Sama.Services.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Sama.Services.Ngos;
using System;

namespace Sama.Api.Controllers
{
    [AllowAnonymous]
    public class NgosController : BaseController
    {
        private readonly INgosService _ngosService;

        public NgosController(ICommandDispatcher commandDispatcher,
            INgosService ngosService) : base(commandDispatcher)
        {
            _ngosService = ngosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ngos = await _ngosService.GetAllAsync();

            return Ok(ngos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ngo = await _ngosService.GetAsync(id);
            
            return Single(ngo);
        }
    }
}