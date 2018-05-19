using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Sama.Infrastructure.Mvc;
using Sama.Services.Dispatchers;
using Sama.Services.Identity.Commands;
using Sama.Services;
using Sama.Services.Identity;
using Sama.Api.Framework;

namespace Sama.Api.Controllers
{
    [Route("")]
    [AllowAnonymous]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public IdentityController(ICommandDispatcher commandDispatcher,
            IIdentityService identityService, IRefreshTokenService refreshTokenService) : base(commandDispatcher)
        {
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpGet("me")]
        [Auth]
        public async Task<IActionResult> Get()
            => Single(await _identityService.GetAsync(UserId));

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp command)
            => await DispatchAsync(command.BindId(c => c.Id));

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
            => Ok(await _identityService.SignInAsync(command.Email, command.Password));

        [HttpPost("refresh-tokens/{refreshToken}/refresh")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(refreshToken));

        [HttpPost("me/funds")]
        [Auth]
        public async Task<IActionResult> Donate([FromBody] AddFunds command)
            => await DispatchAsync(command.Bind(c => c.UserId, UserId));
    }
}