using ePizza.Data.Concrete.EntityFramework.Context;
using ePizza.Entities.Concrete;
using ePizza.Repositories.Interfaces;
using ePizza.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ePizza.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public ePizzaContext ePizzaContext
        {
            //context nesnesini burada kapsule(encapsulation) ederek almış olduk.
            get
            {
                return _context as ePizzaContext;
            }
        }

        public OrderRepository(DbContext context) : base(context)
        {
        }

        public OrderModel GetOrderDetails(string id)
        {
            throw new NotImplementedException();
        }

        public PagingListModel<OrderModel> GetOrderList(int page, int pageSize)
        {
            var pagingModel = new PagingListModel<OrderModel>();
            var data = (from order in ePizzaContext.Orders
                        join payment in ePizzaContext.PaymentDetails
                        on order.PaymentId equals payment.Id
                        select new OrderModel
                        {
                            Id = order.Id,
                            UserId = order.UserId,
                            PaymentId = payment.Id,
                            CreatedDate = order.CreatedDate,
                            GrandTotal = payment.GrandTotal,
                            Locality = order.Locality
                        });
            int itemCount = data.Count();
            var orders = data.Skip((page - 1) * pageSize).Take(pageSize);
            var pagedListData = new StaticPagedList<OrderModel>(orders, page, pageSize, itemCount);
            pagingModel.Data = pagedListData;
            pagingModel.Page = page;
            pagingModel.PageSize = page;
            pagingModel.TotalRows = itemCount;
            return pagingModel;


        }

        public IEnumerable<Order> GetUserOrders(int userId)
        {
            //satışa göre kullanıcıları list ettik
            return ePizzaContext.Orders.Include(o => o.OrderItem).Where(x => x.UserId == userId).ToList();
            
        }
    }
}
