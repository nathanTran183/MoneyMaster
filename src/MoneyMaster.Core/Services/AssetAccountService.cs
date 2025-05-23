﻿using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class AssetAccountService : IAssetAccountService
    {
        private readonly IAssetAccountRepository assetAccountRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public AssetAccountService(IAssetAccountRepository assetAccountRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.assetAccountRepository = assetAccountRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsAsync()
        {
            var assetAccounts = await assetAccountRepository.GetAssetAccountsAsync();
            var result = new ServiceResult<IEnumerable<AssetAccountDTO>>();
            result.Value = mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
            return result;
        }

        public async Task<ServiceResult<AssetAccountDTO>> GetAssetAccountByIdAsync(int id)
        {
            var assetAccount = await assetAccountRepository.GetAssetAccountByIdAsync(id);
            if (assetAccount == null)
            {
                throw new InvalidDataException($"Asset Account with Id = {id} is not found.");
            }
            var result = new ServiceResult<AssetAccountDTO>();
            result.Value = mapper.Map<AssetAccountDTO>(assetAccount);
            return result;
        }

        public async Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByCreatorIdAsync(string creatorId)
        {
            var result = new ServiceResult<IEnumerable<AssetAccountDTO>>();
            var creator = await userRepository.GetUserByIdAsync(creatorId);
            if (creator == null)
            {
                result.AddErrors($"User Id = {creatorId} is not existed.");
                return result;
            }
            var assetAccounts = await assetAccountRepository.GetAssetAccountsByCreatorIdAsync(creatorId);
            result.Value = mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
            return result;
        }

        public async Task<ServiceResult<int>> AddAssetAccountAsync(AssetAccountDTO assetAccountDTO)
        {
            var result = new ServiceResult<int>();
            var isNameUnique = await assetAccountRepository.AssetAccountNameExistByCreatorId(assetAccountDTO.Id, assetAccountDTO.CreatorId, assetAccountDTO.Name);
            if (isNameUnique)
            {
                result.AddErrors($"The Asset Account named {assetAccountDTO.Name} is existed.");
                return result;
            }

            var assetAccount = mapper.Map<AssetAccount>(assetAccountDTO);
            result.Value = await assetAccountRepository.AddAssetAccountAsync(assetAccount);
            return result;
        }

        public async Task<ServiceResult> UpdateAssetAccountAsync(AssetAccountDTO assetAccountDTO)
        {
            var result = new ServiceResult() { Success = true };

            var assetAcc = await assetAccountRepository.GetAssetAccountByIdAsync(assetAccountDTO.Id);
            if (assetAcc == null)
            {
                result.AddErrors($"The Asset Account named {assetAccountDTO.Name} is not existed.");
                return result;
            }

            var isNameUnique = await assetAccountRepository.AssetAccountNameExistByCreatorId(assetAccountDTO.Id, assetAccountDTO.CreatorId, assetAccountDTO.Name);
            if (isNameUnique)
            {
                result.AddErrors($"The Asset Account named {assetAccountDTO.Name} is existed.");
                return result;
            }

            var assetAccount = mapper.Map<AssetAccount>(assetAccountDTO);
            await assetAccountRepository.UpdateAssetAccountAsync(assetAccount);
            return result;
        }

        public async Task<ServiceResult> DeleteAssetAccountAsync(int id)
        {
            var result = new ServiceResult() { Success = true };
            var assetAccount = await assetAccountRepository.GetAssetAccountByIdAsync(id);
            if (assetAccount == null)
            {
                result.AddErrors($"The Asset Account with Id = {id} is not found.");
            }
            else
            {
                await assetAccountRepository.DeleteAssetAccountAsync(assetAccount);
            }
            return result;
        }
    }
}
