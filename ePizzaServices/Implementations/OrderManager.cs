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
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderModel GetOrderDetails(string orderId)
        {
            var model = _orderRepository.GetOrderDetails(orderId);
            if (model != null&&model.Products.Count>0)
            {
                decimal sumTotal = 0;
                foreach (var item in model.Products)
                {
                    item.Total = item.UnitPrice + item.Quantity;
                    sumTotal += item.Total;
                }
                model.Total = sumTotal;
                model.Tax = Math.Round((model.Total+5) / 100, 2);
                model.GrandTotal = model.Tax + model.Total;
            }
            return model;
        }

        public PagingListModel<OrderModel> GetOrderList(int page = 1, int pageSize = 10)
        {
            return _orderRepository.GetOrderList(page, pageSize);
        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            return _orderRepository.GetUserOrders(userId);
        }

        public async Task<int> PlaceOrder(int userId, string orderId, string paymentId, CartModel cart, Address address)
        {
            Order order = new Order
            {
                PaymentId = paymentId,
                UserId = userId,
                CreatedDate = DateTime.Now,
                Id = orderId,
                Street =address.Street,
                Locality = address.Locality,
                City = address.City,
                ZipCode = address.ZipCode,
                PhoneNumber = address.PhoneNumber,
            };
            foreach (var item in cart.Products)
            {
                OrderItem orderItem = new OrderItem(item.ProductId, item.UnitPrice, item.Quantity, item.Total);
            }
            await _orderRepository.AddAsync(order);
            return await _orderRepository.SaveAsync();
        }
    }
}
