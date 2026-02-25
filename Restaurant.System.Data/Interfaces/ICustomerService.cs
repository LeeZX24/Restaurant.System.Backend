using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> IsCustomerAnonymous(Guid CustomerId);

        Task<Customer> GetLatestCustomer();

        Task<Customer> GetCurrentCustomer(Guid CustomerId);

        Task AddNewCustomer(Customer newCustomer);
    }
}