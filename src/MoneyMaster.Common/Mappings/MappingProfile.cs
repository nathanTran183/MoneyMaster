using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Entities;

namespace MoneyMaster.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<AssetAccount, AssetAccountDTO>();
        }
    }
}
