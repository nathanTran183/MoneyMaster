using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Models.Requests;
using MoneyMaster.Common.Models.Responses;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Api.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public class TransactionsController : ControllerBase
{
    readonly ITransactionService transactionService;
    readonly ILogger logger;

    public TransactionsController(ITransactionService transactionService, ILogger logger)
    {
        this.transactionService = transactionService;
        this.logger = logger;
    }

    // GET api/<TransactionsController>
    [HttpGet]
    public async Task<ActionResult<ResponseResult<IEnumerable<TransactionDTO>>>> GetTransactionsAsync()
    {
        var result = await transactionService.GetTransactionsAsync();
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<TransactionDTO>>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<IEnumerable<TransactionDTO>>.CreateError(result.Errors!, "Failed to retrieve all Transactions"));
    }

    // GET api/<TransactionsController>/4
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseResult<TransactionDTO>>> GetTransactionByIdAsync(int id)
    {
        var result = await transactionService.GetTransactionByIdAsync(id);
        if (result.Success)
        {
            return Ok(ResponseResult<TransactionDTO>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<TransactionDTO>.CreateError(result.Errors!, $"Failed to retrieve Transaction with Id = {id}"));
    }

    // GET api/<TransactionsController>/user/4
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<ResponseResult<IEnumerable<TransactionDTO>>>> GetTransactionsByUserIdAsync(string userId)
    {
        var result = await transactionService.GetTransactionsByUserIdAsync(userId);
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<TransactionDTO>>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<IEnumerable<TransactionDTO>>.CreateError(result.Errors!, $"Failed to retrieve Transactions of User Id = {userId}"));
    }

    // GET api/<TransactionsController>/family/4
    [HttpGet("family/{familyId}")]
    public async Task<ActionResult<ResponseResult<IEnumerable<TransactionDTO>>>> GetTransactionsByFamilyIdAsync(int familyId)
    {
        var result = await transactionService.GetTransactionsByFamilyIdAsync(familyId);
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<TransactionDTO>>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<IEnumerable<TransactionDTO>>.CreateError(result.Errors!, $"Failed to retrieve Transactions of family with Id = {familyId}"));
    }

    // GET api/<TransactionsController>/assetAccount/4
    [HttpGet("assetAccount/{assetAccountId}")]
    public async Task<ActionResult<ResponseResult<IEnumerable<TransactionDTO>>>> GetTransactionsByAssetAccountIdAsync(int assetAccountId)
    {
        var result = await transactionService.GetTransactionsByAssetAccountIdAsync(assetAccountId);
        if (result.Success)
        {
            return Ok(ResponseResult<IEnumerable<TransactionDTO>>.CreateSuccess(result.Value));
        }

        return NotFound(ResponseResult<IEnumerable<TransactionDTO>>.CreateError(result.Errors!, $"Failed to retrieve Transactions of Asset Account with Id = {assetAccountId}"));
    }

    // POST api/<TransactionsController>
    [HttpPost]
    public async Task<IActionResult> AddTransactionAsync([FromBody] UpsertTransactionRequest req)
    {
        try
        {
            var transaction = new TransactionDTO
            {
                Amount = req.Amount,
                TransactionType = req.TransactionType,
                TransactionDate = DateTime.Now,
                SubCategoryId = req.SubCategoryId,
                FamilyId = req.FamilyId,
                AssetAccountId = req.AssetAccountId,
                TransferTransactionId = req.TransferTransactionId,
                UserId = req.UserId
            };
            var result = await transactionService.AddTransactionAsync(transaction);
            if (result.Success)
            {
                transaction.Id = result.Value;
                return Ok(ResponseResult<TransactionDTO>.CreateSuccess(transaction));
            }
            else
            {
                return BadRequest(ResponseResult<TransactionDTO>.CreateError(result.Errors!, "Failed to add new Transaction"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while adding Transaction");
        }
    }

    // PUT api/<TransactionsController>/4
    [HttpPut("{TransactionId}")]
    public async Task<IActionResult> UpdateTransactionAsync(int transactionId, [FromBody] UpsertTransactionRequest req)
    {
        try
        {
            var transaction = new TransactionDTO 
            { 
                Id = transactionId, 
                Amount = req.Amount,
                TransactionType = req.TransactionType,
                TransactionDate = req.TransactionDate,
                SubCategoryId = req.SubCategoryId, 
                FamilyId = req.FamilyId,
                AssetAccountId = req.AssetAccountId,
                TransferTransactionId = req.TransferTransactionId,
                UserId = req.UserId 
            };
            var result = await transactionService.UpdateTransactionAsync(transaction);
            if (result.Success)
            {
                return Ok(ResponseResult<TransactionDTO>.CreateSuccess(transaction));
            }
            else
            {
                return BadRequest(ResponseResult<TransactionDTO>.CreateError(result.Errors!, "Failed to update Transaction"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while updating Transaction");
        }
    }

    // DELETE api/<TransactionsController>/5
    [HttpDelete("{TransactionId}")]
    public async Task<ActionResult<TransactionDTO>> DeleteTransactionAsync(int transactionId)
    {
        try
        {
            var result = await transactionService.DeleteTransactionAsync(transactionId);
            if (result.Success)
            {
                return Ok(ResponseResult<object>.CreateSuccess(null));
            }
            else
            {
                return BadRequest(ResponseResult<object>.CreateError(result.Errors!, "Failed to delete Transaction"));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred while deleting Transaction");
        }
    }
}
