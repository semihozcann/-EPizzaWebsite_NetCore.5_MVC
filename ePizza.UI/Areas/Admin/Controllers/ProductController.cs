using ePizza.Services.Interfaces;
using ePizza.Shared.Utilities.ComplexType;
using ePizza.UI.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IProductTypeService _productTypeService;
        private readonly IFileHelper _fileHelper;

        public ProductController(IProductService productService, ICategoryService categoryService, IProductTypeService productTypeService, IFileHelper fileHelper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productTypeService = productTypeService;
            _fileHelper = fileHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _productService.GetAllAsync();
            if (result.Result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Result.Data);
            }
            return NotFound();
        }
    }
}
