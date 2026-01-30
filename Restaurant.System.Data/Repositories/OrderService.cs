using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Customer> _customerRepository;

        public OrderService(IRepository<Customer> CustomerRepository)
        {
            _customerRepository = CustomerRepository;
        }

        public async Task<List<Order>> GetOrdersByCustomer(string CustomerId)
        {
            throw new NotImplementedException();
        }
    }
}