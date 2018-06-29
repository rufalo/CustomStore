using CustomStoreApi.Context;
using CustomStoreApi.Context.Tabels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomStoreApi.Queries
{
    public class PaymentQueries
    {
        private readonly ApplicationContext _Context;

        public PaymentQueries(ApplicationContext context)
        {
            _Context = context;
        }

        public Order NewOrder(Order order)
        {
            _Context.Orders.Add(order);
            _Context.SaveChanges();
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _Context.Orders.ToList();
        }

        public IEnumerable<Order> GetOrdersByUser(ApplicationUser user)
        {
            return _Context.Orders.Where(x => x.ApplicationUser_Id == user.Id);
        }

    }
}
