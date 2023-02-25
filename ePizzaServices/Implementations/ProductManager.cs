using AutoMapper;
using ePizza.En.Dtos.Products;
using ePizza.Entities.Concrete;
using ePizza.Entities.Dtos.Categories;
using ePizza.Entities.Dtos.Products;
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
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(IProductRepository productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<ProductDto>> AddAsync(ProductAddDto productAddDto)
        {
            var product = _mapper.Map<Product>(productAddDto); //öncelikle gelen datayı dto olarak maplemiz gerekmektedir. aksi halde anaymous type olarak giden data hata çözülmeyecektir.
            var productAdded = await _productRepository.AddAsync(product);
            return new DataResult<ProductDto>(ResultStatus.Success, "Product Added", new ProductDto
            {
                Product = productAdded,
                Message = "Product Added",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IDataResult<ProductDto>> DeleteAsync(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (productId != null)
            {
                var deletedProduct = await _productRepository.DeleteAsync(product);
                await _productRepository.SaveAsync();
                return new DataResult<ProductDto>(ResultStatus.Success, "Product is Deleted", new ProductDto
                {
                    Product = deletedProduct,
                    Message = "Product is Deleted",
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<ProductDto>(ResultStatus.Error, "Product Not Found", new ProductDto
                {
                    Product = null,
                    Message = "Product Not Found",
                    ResultStatus= ResultStatus.Error
                });
            }
        }

        public async Task<IDataResult<ProductListDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (products != null)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                    ResultStatus = ResultStatus.Success
                });
            }
            else
            {
                return new DataResult<ProductListDto>(ResultStatus.Error, "Products Not Found", new ProductListDto
                {
                    Products = null,
                    Message = "Products Not Found",
                    ResultStatus = ResultStatus.Error
                });
            }
        }

        public async Task<IDataResult<ProductDto>> GetAsync(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product!=null)
            {
                return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
                {
                    Product = product,
                    ResultStatus = ResultStatus.Success,
                });
            }
            else
            {
                return new DataResult<ProductDto>(ResultStatus.Success, "Kendimi Seviyorum", new ProductDto
                {
                    Product = null,
                    Message = "Kendimi Seviyorum",
                    ResultStatus= ResultStatus.Error,
                });
            }
        }

        public async Task<IDataResult<ProductDto>> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var oldProduct = await _productRepository.GetAsync(x => x.Id == productUpdateDto.Id);
            var product = _mapper.Map<ProductUpdateDto, Product>(productUpdateDto, oldProduct);
            var updatedProduct = await _productRepository.UpdateAsync(product);
            await _productRepository.SaveAsync();
            return new DataResult<ProductDto>(ResultStatus.Success, new ProductDto
            {
                Product = updatedProduct,
                Message = "Product Updated",
                ResultStatus = ResultStatus.Success,
            });
        }
    }
}
