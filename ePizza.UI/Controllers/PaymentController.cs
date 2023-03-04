using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.Services.Models;
using ePizza.UI.Helpers;
using ePizza.UI.Interfaces;
using ePizza.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace ePizza.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IOptions<RazorPayConfig> _razorPayConfing;
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public PaymentController(IUserAccessor userAccessor, IOptions<RazorPayConfig> razorPayConfing, IPaymentService paymentService, IOrderService orderService) : base(userAccessor)
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

        [HttpPost]
        public IActionResult Status(IFormCollection form)
        {
            try
            {
                if (form.Keys.Count>0 && !string.IsNullOrWhiteSpace(form["rzp_payment"]))
                {
                    string paymentId = form["rzp_paymentid"];
                    string orderId = form["rzp_orderid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currency"];

                    var payment = _paymentService.GetPaymentDetails(paymentId);
                    bool ISignVerified = _paymentService.VerifySignature(signature, orderId, paymentId);
                    if (ISignVerified && payment != null)
                    {
                        CartModel cart = TempData.Get<CartModel>("Cart");
                        PaymentDetails model = new PaymentDetails();
                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.GrandTotal = cart.GrandTotal;
                        model.Status = payment.Attributes["status"];
                        model.TransactionId = transactionId;
                        model.Currency = payment.Attributes["currency"];
                        model.Email = payment.Attributes["email"];
                        model.UserId = CurrentUser.Id;

                        var status = Convert.ToInt32(_paymentService.SavePaymentDetails(model));
                        if (status>0)
                        {
                            Response.Cookies.Append("CId", "");
                            Address address = TempData.Get<Address>("Address");
                            _orderService.PlaceOrder(CurrentUser.Id, orderId, paymentId, cart, address);
                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Ödeme alınırken bir hata oluştu";
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            ViewBag.Message = "Ödeme alınırken bir hata oluştu";
            return View();
        }
    }
}
