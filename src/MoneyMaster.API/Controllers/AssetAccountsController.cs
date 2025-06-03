using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Requests.AssetAccount;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AssetAccountsController : ControllerBase
    {
        private readonly IAssetAccountService assetAccountService;
        private readonly ILogger<AssetAccountsController> logger;
        public AssetAccountsController(IAssetAccountService assetAccountService, ILogger<AssetAccountsController> logger)
        {
            this.assetAccountService = assetAccountService;
            this.logger = logger;
        }

        // GET api/<AssetAccountsController>/4
        [HttpGet("{UserId}")]
        public async Task<ActionResult<ResponseResult<IEnumerable<AssetAccountDTO>>>> GetAssetAccountByUserIdAsync(string userId)
        {
            var result = await assetAccountService.GetAssetAccountsByUserIdAsync(userId);
            if (result.Success)
            {
                return Ok(ResponseResult<IEnumerable<AssetAccountDTO>>.CreateSuccess(result.Value));
            }
            
            return NotFound(ResponseResult<IEnumerable<AssetAccountDTO>>.CreateError(result.Errors, "Failed to retrieve Asset Account"));
        }

        // POST api/<AssetAccountsController>
        [HttpPost]
        public async Task<ActionResult<ResponseResult<AssetAccountDTO>>> AddAssetAccountAsync([FromBody] AddAssetAccountRequest req)
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
                    return BadRequest(ResponseResult<AssetAccountDTO>.CreateError(result.Errors, "Failed to retrieve Asset Account"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while adding the asset account");
            }
        }

        // PUT api/<AssetAccountsController>
        [HttpPut]
        public async Task<IActionResult> UpdateAssetAccountAsync([FromBody] UpdateAssetAccountRequest req)
        {
            try
            {
                var assetAccount = new AssetAccountDTO { Name = req.Name, UserId = req.UserId, AssetType = req.AssetType };
                var result = await assetAccountService.UpdateAssetAccountAsync(assetAccount);
                if (result.Success)
                {
                    return Ok(ResponseResult<AssetAccountDTO>.CreateSuccess(assetAccount));
                }
                else
                {
                    return BadRequest(ResponseResult<AssetAccountDTO>.CreateError(result.Errors, "Failed to update Asset Account"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while updating the asset account");
            }
        }
        
        // DELETE api/<AssetAccountsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AssetAccountDTO>> DeleteAssetAccountAsync([FromBody] DeleteAssetAccountRequest req)
        {
            try
            {
                var result = await assetAccountService.DeleteAssetAccountAsync(req.Id);
                if (result.Success)
                {
                    return Ok(ResponseResult<object>.CreateSuccess(null));
                }
                else
                {
                    return BadRequest(ResponseResult<object>.CreateError(result.Errors, "Failed to delete Asset Account"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while deleting the asset account");
            }
        }
    }
}
