using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Requests.Category;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class SubCategoriesController(ISubCategoryService subCategoryService, ILogger logger) : ControllerBase
{
    readonly ILogger logger = logger;
    readonly ISubCategoryService subCategoryService = subCategoryService;

    // GET api/<SubCategoriesController>
    [HttpGet]
    public async Task<IActionResult> GetSubCategories()
    {
        var result = await subCategoryService.GetSubCategoriesAsync();
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<SubCategoryDTO>>.CreateSuccess(result.Value));
        }
        return NotFound(ResponseResult<IEnumerable<SubCategoryDTO>>.CreateError(result.Errors, "Failed to retrieve SubCategories"));
    }

    // POST api/<SubCategoriesController>
    [HttpPost]
    public async Task<IActionResult> AddSubCategory(UpsertSubCategoryRequest req)
    {
        try
        {
            var subCategory = new SubCategoryDTO { Name = req.Name, Icon = req.Icon, CategoryId = req.CategoryId, UserId = req.UserId };
            var result = await subCategoryService.AddSubCategoryAsync(subCategory);
            if (result.Success)
            {
                subCategory.Id = result.Value;
                return Ok(ResponseResult<SubCategoryDTO>.CreateSuccess(subCategory));
            }
            return BadRequest(ResponseResult<SubCategoryDTO>.CreateError(result.Errors, "Failed to add new SubCategory"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while adding the SubCategory");
        }
    }

    // PUT api/<SubCategoriesController>/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubCategory(int id, UpsertSubCategoryRequest req)
    {
        try
        {
            var subCategory = new SubCategoryDTO { Id = id, Name = req.Name, Icon = req.Icon, CategoryId = req.CategoryId, UserId = req.UserId };
            var result = await subCategoryService.UpdateSubCategoryAsync(subCategory);
            if (result.Success)
            {
                return Ok(ResponseResult<SubCategoryDTO>.CreateSuccess(subCategory));
            }
            return BadRequest(ResponseResult<SubCategoryDTO>.CreateError(result.Errors, "Failed to add new SubCategory"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while updating the SubCategory");
        }
    }

    // DELETE api/<SubCategoriesController>/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        try
        {
            var result = await subCategoryService.DeleteSubCategoryAsync(id);
            if (result.Success)
            {
                return Ok(ResponseResult<object>.CreateSuccess(null));
            }
            return BadRequest(ResponseResult<object>.CreateError(result.Errors, "Failed to delete SubCategory"));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while deleting the SubCategory");
        }
    }
}
