using ePizza.Entities.Concrete;
using ePizza.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.En.Dtos.Products
{
    public class ProductListDto : DtoGetBase
    {
        public IList<Product> Products { get; set; }
    }
}
