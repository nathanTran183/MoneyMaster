using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Requests;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class AssetAccountsController : ControllerBase
{
    private readonly IAssetAccountService assetAccountService;
    private readonly ILogger logger;
    public AssetAccountsController(IAssetAccountService assetAccountService, ILogger logger)
    {
        this.assetAccountService = assetAccountService;
        this.logger = logger;
    }

    // GET api/<AssetAccountsController>
    [HttpGet]
    public async Task<ActionResult<ResponseResult<IEnumerable<AssetAccountDTO>>>> GetAssetAccountsAsync()
    {
        var result = await assetAccountService.GetAssetAccountsAsync();
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<AssetAccountDTO>>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<IEnumerable<AssetAccountDTO>>.CreateError(result.Errors!, "Failed to retrieve Asset Accounts"));
    }

    // GET api/<AssetAccountsController>/user/4
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseResult<AssetAccountDTO>>> GetAssetAccountByIdAsync(int id)
    {
        var result = await assetAccountService.GetAssetAccountByIdAsync(id);
        if (result.Success)
        {
            return Ok(ResponseResult<AssetAccountDTO>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<AssetAccountDTO>.CreateError(result.Errors!, $"Failed to retrieve Asset Account with Id = {id}"));
    }

    // GET api/<AssetAccountsController>/user/4
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ResponseResult<IEnumerable<AssetAccountDTO>>>> GetAssetAccountsByUserIdAsync(string userId)
    {
        var result = await assetAccountService.GetAssetAccountsByUserIdAsync(userId);
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<AssetAccountDTO>>.CreateSuccess(result.Value));
        }
        
        return NotFound(ResponseResult<IEnumerable<AssetAccountDTO>>.CreateError(result.Errors!, $"Failed to retrieve Asset Account of User with Id = {userId}"));
    }

    // POST api/<AssetAccountsController>
    [HttpPost]
    public async Task<IActionResult> AddAssetAccountAsync([FromBody] UpsertAssetAccountRequest req)
    {
        try
        {
            var assetAccount = new AssetAccountDTO { Name = req.Name, UserId = req.UserId, AssetType = req.AssetType };
            var result = await assetAccountService.AddAssetAccountAsync(assetAccount);
            if (result.Success)
            {
                assetAccount.Id = result.Value;
                return Ok(ResponseResult<AssetAccountDTO>.CreateSuccess(assetAccount));
            }
            else
            {
                return BadRequest(ResponseResult<AssetAccountDTO>.CreateError(result.Errors!, "Failed to add new Asset Account"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while adding the Asset Account");
        }
    }

    // PUT api/<AssetAccountsController>/4
    [HttpPut("{assetAccountId}")]
    public async Task<IActionResult> UpdateAssetAccountAsync(int assetAccountId, [FromBody] UpsertAssetAccountRequest req)
    {
        try
        {
            var assetAccount = new AssetAccountDTO { Id = assetAccountId, Name = req.Name, UserId = req.UserId, AssetType = req.AssetType };
            var result = await assetAccountService.UpdateAssetAccountAsync(assetAccount);
            if (result.Success)
            {
                return Ok(ResponseResult<AssetAccountDTO>.CreateSuccess(assetAccount));
            }
            else
            {
                return BadRequest(ResponseResult<AssetAccountDTO>.CreateError(result.Errors!, "Failed to update Asset Account"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while updating the Asset Account");
        }
    }
    
    // DELETE api/<AssetAccountsController>/5
    [HttpDelete("{assetAccountId}")]
    public async Task<ActionResult<AssetAccountDTO>> DeleteAssetAccountAsync(int assetAccountId)
    {
        try
        {
            var result = await assetAccountService.DeleteAssetAccountAsync(assetAccountId);
            if (result.Success)
            {
                return Ok(ResponseResult<object>.CreateSuccess(null));
            }
            else
            {
                return BadRequest(ResponseResult<object>.CreateError(result.Errors!, "Failed to delete Asset Account"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while deleting the Asset Account");
        }
    }
}
