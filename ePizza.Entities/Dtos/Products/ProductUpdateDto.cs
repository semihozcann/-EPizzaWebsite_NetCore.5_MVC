using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.En.Dtos.Products
{
    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter UnitPrice")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Please Enter ImageUrl")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please Enter Category")]
        public int CategoryId { get; set; }
        public int ProductTypeId { get; set; }
    }
}
