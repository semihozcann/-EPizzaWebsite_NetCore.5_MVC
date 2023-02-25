

using ePizza.Entities.Concrete;
using ePizza.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Entities.Dtos.Products
{
    public class ProductDto : DtoGetBase 
    {
        public Product Product { get; set; }
    }
}
