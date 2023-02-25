using ePizza.Entities.Concrete;
using ePizza.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Entities.Dtos.ProductTypes
{
    public class ProductTypeDto : DtoGetBase
    {
        public ProductType ProductType { get; set; }
    }
}
