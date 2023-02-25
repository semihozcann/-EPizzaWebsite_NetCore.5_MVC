using ePizza.Entities.Dtos.ProductTypes;
using ePizza.Shared.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Interfaces
{
    public interface IProductTypeService
    {
        Task<IDataResult<ProductTypeListDto>> GetAllAsync();

    }
}
