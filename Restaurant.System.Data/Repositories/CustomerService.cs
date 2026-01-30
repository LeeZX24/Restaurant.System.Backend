using Restaurant.System.Data.Interfaces;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> CustomerRepository)
        {
            _customerRepository = CustomerRepository;
        }

        public async Task<bool> IsCustomerAnonymous(string customerId)
        {
            var customer = (await _customerRepository.GetByFieldAsync(e => e.CustomerId == customerId)).FirstOrDefault();
            return customer.IsAnonymous;
        }

        public async Task<Customer> GetLatestCustomer()
        {
            var customerList = await _customerRepository.GetAllAsync();
            return customerList.Last() ?? null;
        }

        public async Task<Customer> GetCurrentCustomer(string customerId)
        {
            var customer = (await _customerRepository.GetByFieldAsync(e => e.CustomerId == customerId)).FirstOrDefault();
            return customer;
        }
    }
}