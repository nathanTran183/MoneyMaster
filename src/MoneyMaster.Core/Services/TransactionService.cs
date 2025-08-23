using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services;

public class TransactionService : ITransactionService
{
    IMapper mapper;
    ITransactionRepository transactionRepository;

    public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
    {
        this.mapper = mapper;
        this.transactionRepository = transactionRepository;
    }

    public async Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsAsync()
    {
        var result = new ServiceResult<IEnumerable<TransactionDTO>>();
        var transactions = await transactionRepository.GetTransactionsAsync();
        result.Value = mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        return result;
    }

    public async Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByAssetAccountIdAsync(int assetAccountId)
    {
        var result = new ServiceResult<IEnumerable<TransactionDTO>>();
        var transactions = await transactionRepository.GetTransactionsByAssetAccountIdAsync(assetAccountId);
        result.Value = mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        return result;
    }

    public async Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByFamilyIdAsync(int familyId)
    {
        var result = new ServiceResult<IEnumerable<TransactionDTO>>();
        var transactions = await transactionRepository.GetTransactionsByFamilyIdAsync(familyId);
        result.Value = mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        return result;
    }

    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<TransactionDTO>> GetTransactionByIdAsync(int id)
    {
        var result = new ServiceResult<TransactionDTO>();
        var transactions = await transactionRepository.GetTransactionByIdAsync(id);
        result.Value = mapper.Map<TransactionDTO>(transactions);
        return result;
    }

    public async Task<ServiceResult> UpdateTransactionAsync(TransactionDTO transactionDTO)
    {
        var result = new ServiceResult();
        var transaction = await transactionRepository.GetTransactionByIdAsync(transactionDTO.Id);
        if (transaction == null)
        {
            result.AddErrors($"The Transaction with Id = {transactionDTO.Id} is not found.");
        }
        else
        {
            await transactionRepository.UpdateTransactionAsync(transaction);
        }
        return result;
    }

    public async Task<ServiceResult<int>> AddTransactionAsync(TransactionDTO transactionDTO)
    {
        var result = new ServiceResult<int>();
        var transaction = mapper.Map<Transaction>(transactionDTO);
        result.Value = await transactionRepository.AddTransactionAsync(transaction);
        return result;
    }

    public async Task<ServiceResult> DeleteTransactionAsync(int id)
    {
        var result = new ServiceResult();
        var transaction = await transactionRepository.GetTransactionByIdAsync(id);
        if (transaction == null)
        {
            result.AddErrors($"The Transaction with Id = {id} is not found.");
        }
        else
        {
            await transactionRepository.DeleteTransactionAsync(transaction!);
        }
        return result;
    }
}
