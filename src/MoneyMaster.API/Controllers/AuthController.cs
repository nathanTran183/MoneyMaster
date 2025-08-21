using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly ILogger logger;
        readonly IConfiguration configuration;
        readonly IAuthService authService;

        public AuthController(IConfiguration configuration, IAuthService authService, ILogger<AuthController> logger)
        {
            this.configuration = configuration;
            this.authService = authService;
            this.logger = logger;
        }

        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(LoginRequest req)
        //{
        //}

        [HttpPost("register")]
        public async Task<ActionResult<ResponseResult<RegisterResponse>>> RegisterAsync(RegisterRequest req)
        {
            try
            {
                var res = await authService.RegisterUserAsync(req);
                if (res.Success)
                {
                    return Ok(ResponseResult<RegisterResponse>.CreateSuccess(res.Value));
                }
                return BadRequest(ResponseResult<RegisterResponse>.CreateError(res.Errors, "Failed to register new account"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while adding the Category");
            }
            
        }

        [HttpGet("ping")]
        public IActionResult TestConnection()
        {
            return Ok("Pong");
        }
    }
}
