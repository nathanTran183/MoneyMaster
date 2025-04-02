using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace MoneyMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest req)
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterRequest req)
        {

        }
    }
}
