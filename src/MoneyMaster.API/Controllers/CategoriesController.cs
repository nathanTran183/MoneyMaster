using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Requests;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class CategoriesController : ControllerBase
{
    readonly ICategoryService categoryService;
    readonly ILogger logger;

    public CategoriesController(ICategoryService categoryService, ILogger logger)
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

    // GET api/<CategoriesController>/3
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseResult<CategoryDTO>>> GetCategoryByIdAsync(int id)
    {
        var result = await categoryService.GetCategoryByIdAsync(id);
        if (result.Success)
        {
            return Ok(ResponseResult<CategoryDTO>.CreateSuccess(result.Value));
        }
        return NotFound(ResponseResult<CategoryDTO>.CreateError(result.Errors, $"Failed to retrieve Category with Id = {id}"));
    }

    // GET api/<CategoriesController>/user/2
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ResponseResult<IEnumerable<CategoryDTO>>>> GetCategoriesByUserIdAsync(string userId)
    {
        var result = await categoryService.GetCategoriesByUserIdAsync(userId);
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<CategoryDTO>>.CreateSuccess(result.Value));
        }
        return NotFound(ResponseResult<IEnumerable<CategoryDTO>>.CreateError(result.Errors, $"Failed to retrieve Categories by User with Id = {userId}"));
    }

    // POST api/<CategoriesController>
    [HttpPost]
    public async Task<ActionResult<ResponseResult<CategoryDTO>>> AddCategoryAsync([FromBody] UpsertCategoryRequest req)
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

    // PUT api/<CategoriesController>/4
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategoryAsync(int categoryId, [FromBody] UpsertCategoryRequest req)
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

    // DELETE api/<CategorysController>/5
    [HttpDelete("{categoryId}")]
    public async Task<ActionResult<CategoryDTO>> DeleteCategoryAsync(int categoryId)
    {
        try
        {
            var result = await categoryService.DeleteCategoryAsync(categoryId);
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
