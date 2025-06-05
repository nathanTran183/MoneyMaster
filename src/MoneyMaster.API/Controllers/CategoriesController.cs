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
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService categoryService;
        readonly ILogger<CategoriesController> logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            this.categoryService = categoryService;
            this.logger = logger;
        }

        // GET api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<ResponseResult<IEnumerable<CategoryDTO>>>> GetCategoriesAsync()
        {
            var result = await categoryService.GetCategoriesAsync();
            if (result.Success)
            {
                return Ok(ResponseResult<IEnumerable<CategoryDTO>>.CreateSuccess(result.Value));
            }
            return NotFound(ResponseResult<IEnumerable<CategoryDTO>>.CreateError(result.Errors, "Failed to retrieve Categories"));
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult<ResponseResult<CategoryDTO>>> AddCategoryAsync([FromBody] AddAssetAccountRequest req)
        {
            try
            {
                var category = new CategoryDTO { Name = req.Name, UserId = req.UserId };
                var result = await categoryService.AddCategoryAsync(category);
                if (result.Success)
                {
                    category.Id = result.Value;
                    return Ok(ResponseResult<CategoryDTO>.CreateSuccess(category));
                }
                else
                {
                    return BadRequest(ResponseResult<CategoryDTO>.CreateError(result.Errors, "Failed to add new Category"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while adding the Category");
            }
        }

        // PUT api/<CategoriesController>
        [HttpPut]
        public async Task<IActionResult> UpdateAssetAccountAsync([FromBody] UpdateAssetAccountRequest req)
        {
            try
            {
                var category = new CategoryDTO { Name = req.Name, UserId = req.UserId };
                var result = await categoryService.UpdateCategoryAsync(category);
                if (result.Success)
                {
                    return Ok(ResponseResult<CategoryDTO>.CreateSuccess(category));
                }
                else
                {
                    return BadRequest(ResponseResult<CategoryDTO>.CreateError(result.Errors, "Failed to update Category"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while updating the Category");
            }
        }

        // DELETE api/<AssetAccountsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> DeleteAssetAccountAsync([FromBody] DeleteAssetAccountRequest req)
        {
            try
            {
                var result = await categoryService.DeleteCategoryAsync(req.Id);
                if (result.Success)
                {
                    return Ok(ResponseResult<object>.CreateSuccess(null));
                }
                else
                {
                    return BadRequest(ResponseResult<object>.CreateError(result.Errors, "Failed to delete Category"));
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(500, "An error occurred while deleting the Category");
            }
        }
    }
}
