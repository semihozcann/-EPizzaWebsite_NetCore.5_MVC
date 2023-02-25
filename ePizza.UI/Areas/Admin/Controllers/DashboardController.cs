using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private IOrderService _orderService;

        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index(int page=1, int pageSize=2)
        {
            var orders = _orderService.GetOrderList(page, pageSize);
            return View(orders);
        }

        public IActionResult Details(string orderId)
        {
            OrderModel order = _orderService.GetOrderDetails(orderId);
            return View(order);
        }
    }
}
