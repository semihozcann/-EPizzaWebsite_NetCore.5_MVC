using ePizza.Entities.Concrete;
using ePizza.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizza.Repositories.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCart(Guid cartId);
        CartModel GetCartDetails(Guid cartId);
        int DeleteItem(Guid cartId, int productId);
        int UpdateQuantity(Guid cartId, int productId, int quantity);
        int UpdateToCart(Guid cartId, int userId);
    }
}
