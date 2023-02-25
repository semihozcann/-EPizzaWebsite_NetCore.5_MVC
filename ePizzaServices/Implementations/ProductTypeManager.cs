using AutoMapper;
using ePizza.Entities.Dtos.ProductTypes;
using ePizza.Repositories.Interfaces;
using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.Abstract;
using ePizza.Shared.Utilities.ComplexType;
using ePizza.Shared.Utilities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implementations
{
    public class ProductTypeManager : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductTypeManager(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<ProductTypeListDto>> GetAllAsync()
        {
            var productTypes = await _productTypeRepository.GetAllAsync();
            if (productTypes !=null)
            {
                return new DataResult<ProductTypeListDto>(ResultStatus.Success, new ProductTypeListDto
                {
                    ProductTypes = productTypes,
                    ResultStatus = ResultStatus.Success,
                });
            }
            else
            {
                return new DataResult<ProductTypeListDto>(ResultStatus.Error, "Product Type Not Found", new ProductTypeListDto
                {
                    ProductTypes = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Product Type Not Found"
                });
            }
        }
    }
}
