using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class AssetAccountService : IAssetAccountService
    {
        private readonly IAssetAccountRepository _assetAccountRepository;
        private readonly IMapper _mapper;

        public AssetAccountService(IAssetAccountRepository assetAccountRepository, IMapper mapper)
        {
            _assetAccountRepository = assetAccountRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccounts()
        {
            var assetAccounts = await _assetAccountRepository.GetAssetAccountsAsync();
            var result = new ServiceResult<IEnumerable<AssetAccountDTO>>();
            result.Value = _mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
            return result;
        }

        public async Task<ServiceResult<AssetAccountDTO>> GetAssetAccountById(int id)
        {
            var assetAccount = await _assetAccountRepository.GetAssetAccountByIdAsync(id);
            if (assetAccount == null)
            {
                throw new InvalidDataException($"Asset Account with Id = {id} is not found.");
            }
            var result = new ServiceResult<AssetAccountDTO>();
            result.Value = _mapper.Map<AssetAccountDTO>(assetAccount);
            return result;
        }

        public async Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByCreatorId(string creatorId)
        {
            var result = new ServiceResult<IEnumerable<AssetAccountDTO>>();
            var assetAccounts = await _assetAccountRepository.GetAssetAccountsByCreatorIdAsync(creatorId);
            result.Value = _mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
            return result;
        }

        public async Task<ServiceResult<int>> CreateAssetAccount(AssetAccountDTO assetAccountDTO, string userId)
        {
            var isNameUnique = await _assetAccountRepository.AssetAccountNameExistByCreatorId(assetAccountDTO.Id, userId, assetAccountDTO.Name);
            if (isNameUnique)
            {
                throw new InvalidDataException($"The Asset Account named {assetAccountDTO.Name} is existed.");
            }

            var assetAccount = _mapper.Map<AssetAccount>(assetAccountDTO);
            var result = new ServiceResult<int>();
            result.Value = await _assetAccountRepository.AddAssetAccountAsync(assetAccount);
            return result;
        }

        public async Task<ServiceResult> UpdateAssetAccount(AssetAccountDTO assetAccountDTO, string userId)
        {
            var assetAcc = await _assetAccountRepository.GetAssetAccountByIdAsync(assetAccountDTO.Id);
            if (assetAcc == null)
            {
                throw new InvalidOperationException($"The Asset Account named {assetAccountDTO.Name} is not existed.");
            }

            var isNameUnique = await _assetAccountRepository.AssetAccountNameExistByCreatorId(assetAccountDTO.Id, userId, assetAccountDTO.Name);
            if (isNameUnique)
            {
                throw new InvalidDataException($"The Asset Account named {assetAccountDTO.Name} is existed.");
            }

            var assetAccount = _mapper.Map<AssetAccount>(assetAccountDTO);
            await _assetAccountRepository.UpdateAssetAccountAsync(assetAccount);
            var result = new ServiceResult() { Success = true };
            return result;
        }

        public async Task<ServiceResult> DeleteAssetAccount(int id)
        {
            var assetAccount = await _assetAccountRepository.GetAssetAccountByIdAsync(id);
            if (assetAccount == null)
            {
                throw new InvalidOperationException($"The Asset Account with Id = {id} is not found.");
            }

            await _assetAccountRepository.DeleteAssetAccountAsync(assetAccount);
            var result = new ServiceResult() { Success = true };
            return result;
        }
    }
}
