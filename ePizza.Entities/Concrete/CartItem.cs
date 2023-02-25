using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ePizza.Entities.Concrete
{
    public class CartItem
    {
        public CartItem()
        {

        }

        public CartItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public int Id { get; set; }
        public Guid CartId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore] //ajax işlemleri yapıldığı için burada json ignore attribute kullandık.
        public Cart Cart { get; set; }
    }
}
