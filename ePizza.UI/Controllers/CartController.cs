using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using ePizza.UI.Helpers;
using ePizza.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace ePizza.UI.Controllers
{
    public class CartController : BaseController
    {
        readonly ICartService _cartService;
        public CartController(IUserAccessor userAccessor, ICartService cartService) : base(userAccessor)
        {
            _cartService = cartService;
        }

        Guid cartId
        {
            get
            {
                Guid id;
                string CId = Request.Cookies["CId"];
                if (string.IsNullOrEmpty(CId))
                {
                    id = Guid.NewGuid();
                    string guidType = id.ToString();
                    Response.Cookies.Append("CId", guidType);
                }
                else
                {
                    id = Guid.Parse(CId);
                }
                return id;
            }
        }

        public IActionResult Index()
        {
            CartModel cart = _cartService.GetCartDetails(cartId);
            if (CurrentUser!=null && cart != null)
            {
                TempData.Set("Cart", cart);
                _cartService.UpdateCart(cart.Id, CurrentUser.Id);
            }
            return View(cart);
        }

        [Route("Cart/AddToCart/{productId}/{unitPrice}/{quantity}")]
        public IActionResult AddToCart(int productId, decimal unitPrice, int quantity)
        {
            int userId = CurrentUser != null ? CurrentUser.Id : 0;
            if (productId>0 && quantity>0)
            {
                var x = cartId;
                Cart cart = _cartService.AddItem(userId, cartId, productId, unitPrice, quantity);
                var data = JsonSerializer.Serialize(cart);
                return Json(data);
            }
            else
            {
                return Json("");
            }
        }

        public IActionResult DeleteItem(int id)
        {
            int count = _cartService.DeleteItem(cartId,id);
            return Json(count);
        }

        [Route("Cart/UpdateQuantity/{id}/{Quantity}")]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var count = _cartService.UpdateQuantity(cartId,id,quantity);
            return Json(count);
        }

        public IActionResult GetCartCount()
        {
            int count = _cartService.GetCartCount(cartId);
            return Json(count);
        }

        [HttpPost]
        public IActionResult CheckOut(Address address)
        {
            TempData.Set("Address", address);
            return RedirectToAction("Index", "Payment");
        }
    }
}
