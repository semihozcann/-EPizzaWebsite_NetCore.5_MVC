using ePizza.Data.Concrete.EntityFramework.Context;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {

        public ePizzaContext ePizzaContext
        {
            //context nesnesini burada kapsule(encapsulation) ederek almış olduk.
            get
            {
                return _context as ePizzaContext;
            }
        }

        public CartRepository(DbContext context) : base(context)
        {
        }

        public int DeleteItem(Guid cartId, int productId)
        {
            var item = ePizzaContext.CartItems.FirstOrDefault(c => c.CartId == cartId && c.Id == productId);
            if (item!=null)
            {
                ePizzaContext.CartItems.Remove(item);
                return ePizzaContext.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public Cart GetCart(Guid cartId)
        {
            return ePizzaContext.Carts.Include
                ("Product").FirstOrDefault(x => x.Id ==  cartId&& x.IsActive == true);
        }

        public CartModel GetCartDetails(Guid cartId)
        {
            var model = (from cart in ePizzaContext.Carts
                         where cart.Id == cartId && cart.IsActive == true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = DateTime.Now,
                             Products = (from cartItem in ePizzaContext.CartItems
                                         join item in ePizzaContext.Products
                                         on cartItem.ProductId equals item.Id
                                         where cartItem.CartId == cartId
                                         select new ProductModel()
                                         {
                                             Id = cartItem.Id,
                                             Name = item.Name,
                                             Description = item.Description,
                                             ImageUrl = item.ImageUrl,
                                             ProductId = item.Id,
                                             UnitPrice = item.UnitPrice
                                         }).ToList()
                         }).FirstOrDefault();
            return model;
        }

        public int UpdateQuantity(Guid cartId, int productId, int quantity)
        {
            bool flag = false;
            var cart = GetCart(cartId); //secilen ürünü getirir
            if (cart!=null) //urun var ise
            {
                for (int i = 0; i < cart.Products.Count; i++) //sepet içindeki nesneleri dön
                {
                    if (cart.Products[i].Id == productId) // sepet içindeki ürün gelen ürünün ıd sine eşitse
                    {
                        flag = true; // bayrağı true yap
                        if (quantity<0 && cart.Products[i].Quantity>1) //miktar küçükse sıfırdan ve mevcut sepet büyükse 1 den
                        {
                            cart.Products[i].Quantity += (quantity); // mevcut sepete gelen miktarı ekle
                        }
                        else if (quantity >0) //mevcut sepet büyük ise 0 dan
                        {
                            cart.Products[i].Quantity += (quantity); //gelen değeri mevcut sepete ekle 
                        }
                        break; //işi bitir
                    }
                }
                if (flag) //bayrak true ise sql e kaydet
                {
                    return ePizzaContext.SaveChanges();
                }
            }
            return 0; //return 0 döndür eğer ürün yada sepet yoksa
        }

        public int UpdateToCart(Guid cartId, int userId)
        {
            Cart cart = GetCart(cartId);
            cart.UserId = userId;
            return ePizzaContext.SaveChanges();
        }
    }
}
