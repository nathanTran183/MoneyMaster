using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;
using System.Transactions;

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

    public async Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactions()
    {
        var result = new ServiceResult<IEnumerable<TransactionDTO>>();
        var transactions = await transactionRepository.GetTransactionsAsync();
        result.Value = mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        return result;
    }

    public async Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByAssetAccountId(int assetAccountId)
    {
        var result = new ServiceResult<IEnumerable<TransactionDTO>>();
        var transactions = await transactionRepository.GetTransactionsByAssetAccountIdAsync(assetAccountId);
        result.Value = mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        return result;
    }

    public async Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByFamilyId(int familyId)
    {
        var result = new ServiceResult<IEnumerable<TransactionDTO>>();
        var transactions = await transactionRepository.GetTransactionsByFamilyIdAsync(familyId);
        result.Value = mapper.Map<IEnumerable<TransactionDTO>>(transactions);
        return result;
    }

    public Task<ServiceResult<IEnumerable<TransactionDTO>>> GetTransactionsByUserId(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateTransaction(TransactionDTO transactionDTO)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<int>> AddTransaction(TransactionDTO transactionDTO)
    {
        var result = new ServiceResult<int>();
        var transaction = await transactionRepository.GetTransactionByIdAsync(id);

    }

    public async Task<ServiceResult> DeleteTransaction(int id)
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
