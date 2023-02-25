using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using ePizza.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Services.Implementations
{
    public class CartManager : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartManager(ICartRepository cartRepository, IRepository<CartItem> cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public Cart AddItem(int userId, Guid cartId, int productId, decimal unitPrice, int quantity)
        {
            try
            {
                Cart cart = _cartRepository.GetCart(cartId);
                if (cart==null)
                {
                    cart = new Cart();
                    CartItem cartItem = new CartItem();
                    cart.Id = cartId;
                    cart.UserId = userId;
                    cart.CreatedDate = DateTime.Now;

                    cartItem.CartId = cart.Id;
                    cart.Products.Add(cartItem);
                    _cartRepository.AddAsync(cart);
                    _cartRepository.SaveAsync();
                }
                else
                {
                    CartItem product = cart.Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product!=null)
                    {
                        product.Quantity += quantity;
                        _cartItemRepository.UpdateAsync(product);
                        _cartItemRepository.SaveAsync();
                    }
                }
                return cart;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public int DeleteItem(Guid cartId, int id, int itemId)
        {
            return _cartRepository.DeleteItem(cartId, itemId);
        }

        public int GetCartCount(Guid cartId)
        {
            var cart = _cartRepository.GetCart(cartId);
            return cart != null? cart.Products.Count() : 0;
        }

        public CartModel GetCartDetails(Guid cartId)
        {
            var model = _cartRepository.GetCartDetails(cartId);
            if (model!=null&&model.Products.Count>0)
            {
                decimal subTotal = 0;
                foreach (var item in model.Products)
                {
                    item.Total = item.UnitPrice = item.Quantity;
                    subTotal += item.Total;
                }
                model.Total = subTotal;
                model.Tax = Math.Round(model.Total * 5);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;
        }

        public int UpdateCart(Guid cartId, int userId)
        {
            return _cartRepository.UpdateToCart(cartId, userId);
        }

        public int UpdateQuantity(Guid cartId, int id ,int quantity)
        {
            return _cartRepository.UpdateQuantity(cartId, id, quantity);
        }
    }
}
