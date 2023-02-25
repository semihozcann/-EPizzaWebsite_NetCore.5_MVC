namespace ePizza.Entities.Concrete
{
    public class OrderItem
    {
        public OrderItem()
        {

        }

        public OrderItem(int productId, decimal unitPrice, int quantity, decimal total)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}