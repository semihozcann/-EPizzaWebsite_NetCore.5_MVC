using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.Services.Models;
using ePizza.UI.Helpers;
using ePizza.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace ePizza.UI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOptions<RazorPayConfig> _razorPayConfing;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public PaymentController(IOptions<RazorPayConfig> razorPayConfing, IPaymentService paymentService, IOrderService orderService)
        {
            _razorPayConfing = razorPayConfing;
            _paymentService = paymentService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            PaymentViewModel payment = new PaymentViewModel();
            CartModel cart = TempData.Peek<CartModel>("Cart");
            if (cart != null)
            {
                payment.Cart = cart;
            }
            payment.GrandTotal = Math.Round(cart.GrandTotal);
            payment.Currency = "INR";
            string items = "";
            foreach (var item in cart.Products)
            {
                items = item.Name + ",";
            }
            payment.Description = items;
            payment.RazorPayKey = _razorPayConfing.Value.Key;
            payment.Receipt = Guid.NewGuid().ToString();

            payment.OrderId = _paymentService.CreateOrder(payment.GrandTotal + 100, payment.Currency, payment.Receipt);

            return View(payment);
        }
    }
}
