using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ePizza.UI.Areas.Users.Controllers
{
    public class OrderController : BaseController
    {

        private readonly IOrderService _orderService;

        public OrderController(IUserAccessor userAccessor, IOrderService orderService) : base(userAccessor)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetUserOrders(CurrentUser.Id);
            if (orders != null)
            {
                return View(orders);
            }
            return View();
        }

        [Route("~/Users/Order/Details/{orderId}")]
        public IActionResult Details(string orderId)
        {
            OrderModel order = _orderService.GetOrderDetails(orderId);
            return View(order);
        }
    }
}
