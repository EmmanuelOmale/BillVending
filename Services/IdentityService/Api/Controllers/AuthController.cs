using Application;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IIdentityAppService _identityAppService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IIdentityAppService identityAppService)
        {
            _logger = logger;
            _identityAppService = identityAppService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await _identityAppService.RegisterAsync(request);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _identityAppService.LoginAsync(request);
            return Ok(response);
        }
    }
}