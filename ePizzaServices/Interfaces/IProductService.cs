using ePizza.En.Dtos.Products;
using ePizza.Entities.Dtos.Products;
using ePizza.Shared.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Interfaces
{
    public interface IProductService
    {
        Task<IDataResult<ProductDto>> GetAsync(int productId);
        Task<IDataResult<ProductListDto>> GetAllAsync();
        Task<IDataResult<ProductDto>> AddAsync(ProductAddDto productAddDto);
        Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<IDataResult<ProductDto>> DeleteAsync(int productId);

        

    }
}
