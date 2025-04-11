using AutoMapper;
using MoneyMaster.Common.DTOs;a
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
            var result = new ServiceResult<IEnumerable<AssetAccountDTO>>();
            var assetAccounts = await _assetAccountRepository.GetAssetAccountsAsync();
            result.Value = _mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
            return result;
        }

        public async Task<ServiceResult<AssetAccountDTO>> GetAssetAccountById(int id)
        {
            var result = new ServiceResult<AssetAccountDTO>();
            var assetAccount = await _assetAccountRepository.GetAssetAccountByIdAsync(id);
            if (assetAccount == null)
            {
                result.AddErrors("");
            }
            result.Value = _mapper.Map<AssetAccountDTO>(assetAccount);
            return result;
        }

        public async Task<ServiceResult<IEnumerable<AssetAccountDTO>>> GetAssetAccountsByCreatorId(string creatorId)
        {
            var assetAccounts = await _assetAccountRepository.GetAssetAccountsByCreatorIdAsync(creatorId);
            return _mapper.Map<IEnumerable<AssetAccountDTO>>(assetAccounts);
        }

        public Task<ServiceResult<int>> CreateAssetAccount(AssetAccountDTO assetAccountDTO)
        {
            var result = new ServiceResult<AssetAccountDTO>();

            var assetAccount = _assetAccountRepository.AddAssetAccountAsync
        }

        public async Task<ServiceResult> UpdateAssetAccount(AssetAccountDTO assetAccountDTO)
        {
            var result = new ServiceResult();
            await _assetAccountRepository.UpdateAssetAccountAsync()

            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAssetAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
