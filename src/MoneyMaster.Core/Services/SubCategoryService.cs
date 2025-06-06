using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        readonly ISubCategoryService subCategoryService;
        readonly IMapper mapper;

        public SubCategoryService(ISubCategoryService subCategoryService, IMapper mapper)
        {
            this.subCategoryService = subCategoryService;
            this.mapper = mapper;
        }

        public Task<ServiceResult<IEnumerable<SubCategoryDTO>>> GetSubCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<SubCategoryDTO>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<int>> AddSubCategoryAsync(SubCategoryDTO subCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteSubCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
