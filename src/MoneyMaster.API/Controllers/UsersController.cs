using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Database;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly MoneyMasterContext _moneyMasterContext;
    public UsersController(MoneyMasterContext moneyMasterContext)
    {
        _moneyMasterContext = moneyMasterContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        if (_moneyMasterContext.Users == null)
        {
            return NotFound();
        }
        return await _moneyMasterContext.Users.ToListAsync();
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<User>> GetUserById(string id)
    {
        if (_moneyMasterContext.Users == null)
        {
            return NotFound();
        }

        var user = await _moneyMasterContext.Users.SingleOrDefaultAsync(s => s.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}
