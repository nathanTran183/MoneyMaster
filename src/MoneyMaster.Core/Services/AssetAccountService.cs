using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Common.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class AssetAccountService : IAssetAccountService
    {
        private readonly IAssetAccountRepository assetAccountRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public AssetAccountService(IMapper mapper, IAssetAccountRepository assetAccountRepository, IUserRepository userRepository)
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
            var result = new ServiceResult<AssetAccountDTO>();
            var assetAccount = await assetAccountRepository.GetAssetAccountByIdAsync(id);
            if (assetAccount == null)
            {
                result.AddErrors($"Asset Account with Id = {id} is not found.");
            }
            else
            {
                result.Value = mapper.Map<AssetAccountDTO>(assetAccount);
            }
            return result;
        }

        public async Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByUserIdAsync(string userId)
        {
            var result = new ServiceResult<IEnumerable<AssetAccountDTO>>();
            var user = await userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                result.AddErrors($"User Id = {userId} is not existed.");
                return result;
            }
            var assetAccounts = await assetAccountRepository.GetAssetAccountsByUserIdAsync(userId);
            result.Value = mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
            return result;
        }

        public async Task<ServiceResult<int>> AddAssetAccountAsync(AssetAccountDTO assetAccountDTO)
        {
            var result = new ServiceResult<int>();
            var isNameUnique = await assetAccountRepository.AssetAccountNameExistByUserId(assetAccountDTO.Id, assetAccountDTO.UserId, assetAccountDTO.Name);
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
            var result = new ServiceResult();

            var assetAccount = await assetAccountRepository.GetAssetAccountByIdAsync(assetAccountDTO.Id);
            if (assetAccount == null)
            {
                result.AddErrors($"The Asset Account named {assetAccountDTO.Name} is not existed.");
                return result;
            }

            var isNameUnique = await assetAccountRepository.AssetAccountNameExistByUserId(assetAccountDTO.Id, assetAccountDTO.UserId, assetAccountDTO.Name);
            if (isNameUnique)
            {
                result.AddErrors($"The Asset Account named {assetAccountDTO.Name} is existed.");
                return result;
            }

            assetAccount = mapper.Map<AssetAccount>(assetAccountDTO);
            await assetAccountRepository.UpdateAssetAccountAsync(assetAccount);
            return result;
        }

        public async Task<ServiceResult> DeleteAssetAccountAsync(int id)
        {
            var result = new ServiceResult();
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
