using AutoMapper;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos.Categories;
using ePizza.Repositories.Interfaces;
using ePizza.Shared.Utilities.Abstract;
using ePizza.Shared.Utilities.ComplexType;
using ePizza.Shared.Utilities.Concrete;
using ePizza.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implementations
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            var categoryAdded = await _categoryRepository.AddAsync(category);
            return new DataResult<CategoryDto>(ResultStatus.Success, "Category Added", new CategoryDto
            {
                Category = categoryAdded,
                Message = "Cagetory Added",
                ResultStatus = ResultStatus.Success

            });
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == categoryId);
            if (categoryId != null)
            {
                var deletedCategory = await _categoryRepository.DeleteAsync(category);
                await _categoryRepository.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, "Category Deleted", new CategoryDto
                {
                    Category = deletedCategory,
                    Message = "Category Deleted",
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<CategoryDto>(ResultStatus.Error, "Category Not Found", new CategoryDto
                {
                    Category = null,
                    Message = "Category Not Found",
                    ResultStatus = ResultStatus.Error
                });
            }
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories != null)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<CategoryListDto>(ResultStatus.Error, "Categories Not Found", new CategoryListDto
                {
                    Categories = null,
                    Message = "Categories Not Found",
                    ResultStatus= ResultStatus.Error
                });
            }
        }

        public Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
