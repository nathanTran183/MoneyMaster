using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Requests;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class BudgetsController : ControllerBase
{
    private readonly IBudgetService budgetService;
    private readonly ILogger logger;
    public BudgetsController(IBudgetService budgetService, ILogger logger)
    {
        this.budgetService = budgetService;
        this.logger = logger;
    }

    // GET api/<BudgetsController>
    [HttpGet]
    public async Task<ActionResult<ResponseResult<IEnumerable<BudgetDTO>>>> GetBudgetsAsync()
    {
        var result = await budgetService.GetBudgetsAsync();
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<BudgetDTO>>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<IEnumerable<BudgetDTO>>.CreateError(result.Errors!, "Failed to retrieve Budgets"));
    }

    // GET api/<BudgetsController>/4
    [HttpGet("{userId}")]
    public async Task<ActionResult<ResponseResult<IEnumerable<BudgetDTO>>>> GetBudgetsByUserIdAsync(string userId)
    {
        var result = await budgetService.GetBudgetsByUserIdAsync(userId);
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<BudgetDTO>>.CreateSuccess(result.Value));
        }
        
        return NotFound(ResponseResult<IEnumerable<BudgetDTO>>.CreateError(result.Errors!, $"Failed to retrieve Budget with User Id = {userId}"));
    }

    // POST api/<BudgetsController>
    [HttpPost]
    public async Task<IActionResult> AddBudgetAsync([FromBody] UpsertBudgetRequest req)
    {
        try
        {
            var budget = new BudgetDTO { Amount = req.Amount, Month = req.Month, SubCategoryId = req.SubCategoryId, UserId = req.UserId };
            var result = await budgetService.AddBudgetAsync(budget);
            if (result.Success)
            {
                budget.Id = result.Value;
                return Ok(ResponseResult<BudgetDTO>.CreateSuccess(budget));
            }
            else
            {
                return BadRequest(ResponseResult<BudgetDTO>.CreateError(result.Errors!, "Failed to add new Budget"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while adding Budget");
        }
    }

    // PUT api/<BudgetsController>/4
    [HttpPut("{budgetId}")]
    public async Task<IActionResult> UpdateBudgetAsync(int budgetId, [FromBody] UpsertBudgetRequest req)
    {
        try
        {
            var budget = new BudgetDTO { Id = budgetId, Amount = req.Amount, Month = req.Month, SubCategoryId = req.SubCategoryId, UserId = req.UserId };
            var result = await budgetService.UpdateBudgetAsync(budget);
            if (result.Success)
            {
                return Ok(ResponseResult<BudgetDTO>.CreateSuccess(budget));
            }
            else
            {
                return BadRequest(ResponseResult<BudgetDTO>.CreateError(result.Errors!, "Failed to update Budget"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while updating Budget");
        }
    }
    
    // DELETE api/<BudgetsController>/5
    [HttpDelete("{budgetId}")]
    public async Task<ActionResult<BudgetDTO>> DeleteBudgetAsync(int budgetId)
    {
        try
        {
            var result = await budgetService.DeleteBudgetAsync(budgetId);
            if (result.Success)
            {
                return Ok(ResponseResult<object>.CreateSuccess(null));
            }
            else
            {
                return BadRequest(ResponseResult<object>.CreateError(result.Errors!, "Failed to delete Budget"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while deleting Budget");
        }
    }
}
