using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.AuthDto;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager):ApiBaseController
    {
      
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginRequest request,CancellationToken cancellationToken)
        {
            var user = await _serviceManager.AuthService.LoginAsync(request,cancellationToken);

            return Ok(user);

        }
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> RegisterAsync([FromBody] RegisterRequest request,CancellationToken cancellationToken)
        {
            var user = await _serviceManager.AuthService.RegisterAsync(request,cancellationToken);
            return Ok(user);

        }

        [HttpPost("CreateAgent")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<RegisterResponse>> CreateAgentAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = await _serviceManager.AuthService.CreateSupportAgent(request, cancellationToken);
            return Ok(user);

        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserDto>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _serviceManager.AuthService.GetRefreshTokenAsync(request.Token,request.RefreshToken, cancellationToken);
            return Ok(user);
        }
    }
}
