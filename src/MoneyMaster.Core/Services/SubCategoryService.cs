using AutoMapper;
using MoneyMaster.Common.DTOs;
using MoneyMaster.Database.Entities;
using MoneyMaster.Database.Interfaces;
using MoneyMaster.Service.Interfaces;

namespace MoneyMaster.Service.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        readonly ISubCategoryRepository subCategoryRepository;
        readonly ICategoryRepository categoryRepository;
        readonly IMapper mapper;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.subCategoryRepository = subCategoryRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<SubCategoryDTO>>> GetSubCategoriesAsync()
        {
            var result = new ServiceResult<IEnumerable<SubCategoryDTO>>();
            var subCategories = await subCategoryRepository.GetSubCategoriesAsync();
            result.Value = mapper.Map<IEnumerable<SubCategoryDTO>>(subCategories);
            return result;
        }

        public async Task<ServiceResult<IEnumerable<SubCategoryDTO>>> GetSubCategoriesByCategoryIdAsync(int categoryId)
        {
            var result = new ServiceResult<IEnumerable<SubCategoryDTO>>();
            var category = await categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                result.AddErrors($"Category Id = {categoryId} is not existed");
                return result;
            }

            var subCategories = await subCategoryRepository.GetSubCategoriesByCategoryIdAsync(categoryId);
            result.Value = mapper.Map<IEnumerable<SubCategoryDTO>>(subCategories);
            return result;
        }

        public async Task<ServiceResult<int>> AddSubCategoryAsync(SubCategoryDTO subCategoryDTO)
        {
            var result = new ServiceResult<int>();
            var isSubCategoryExisted = await subCategoryRepository.SubCategoryNameExistByCategoryId(subCategoryDTO.Id, subCategoryDTO.CategoryId, subCategoryDTO.Name);
            if (isSubCategoryExisted)
            {
                result.AddErrors($"SubCategory named {subCategoryDTO.Name} is existed.");
                return result;
            }

            var subCategory = mapper.Map<SubCategory>(subCategoryDTO);
            result.Value = await subCategoryRepository.AddSubCategoryAsync(subCategory);
            return result;
        }

        public async Task<ServiceResult> UpdateSubCategoryAsync(SubCategoryDTO subCategoryDTO)
        {
            var result = new ServiceResult();
            var subCategory = await subCategoryRepository.GetSubCategoryByIdAsync(subCategoryDTO.Id);
            if (subCategory == null)
            {
                result.AddErrors($"SubCategory Id = {subCategoryDTO.Id} is not existed");
                return result;
            }
            var isSubCategoryExisted = await subCategoryRepository.SubCategoryNameExistByCategoryId(subCategoryDTO.Id, subCategoryDTO.CategoryId, subCategoryDTO.Name);
            if (isSubCategoryExisted)
            {
                result.AddErrors($"SubCategory named {subCategoryDTO.Name} is existed.");
                return result;
            }

            subCategory = mapper.Map<SubCategory>(subCategoryDTO);
            await subCategoryRepository.UpdateSubCategoryAsync(subCategory);
            return result;
        }

        public async Task<ServiceResult> DeleteSubCategoryAsync(int id)
        {
            var result = new ServiceResult();
            var subCategory = await subCategoryRepository.GetSubCategoryByIdAsync(id);
            if (subCategory == null)
            {
                result.AddErrors($"The SubCategory with Id = {id} not found.");
            }
            else
            {
                await subCategoryRepository.DeleteSubCategoryAsync(subCategory);
            }
            return result;
        }
    }
}
