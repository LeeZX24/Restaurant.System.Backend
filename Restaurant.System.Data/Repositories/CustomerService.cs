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

        public async Task<bool> IsCustomerAnonymous(Guid customerId)
        {
            var customer = (await _customerRepository.GetByFieldAsync(e => e.CustomerId == customerId)).FirstOrDefault();
            return customer.IsAnonymous;
        }

        public async Task<Customer> GetLatestCustomer()
        {
            var customerList = await _customerRepository.GetAllAsync();
            return customerList.Last() ?? null;
        }

        public async Task<Customer> GetCurrentCustomer(Guid customerId)
        {
            var customer = (await _customerRepository.GetByFieldAsync(e => e.CustomerId == customerId)).FirstOrDefault();
            return customer;
        }

        public async Task AddNewCustomer(Customer newCustomer)
        {
            await _customerRepository.AddAsync(newCustomer);
            await _customerRepository.SaveChangesAsync();
        }
    }
}