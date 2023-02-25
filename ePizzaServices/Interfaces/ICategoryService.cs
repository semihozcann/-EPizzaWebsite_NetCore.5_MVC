using ePizza.En.Dtos.Products;
using ePizza.Entities.Dtos.Categories;
using ePizza.Entities.Dtos.Products;
using ePizza.Shared.Utilities.Abstract;
using ePizza.Shared.Utilities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto);
        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId);

    }
}
