using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> IsCustomerAnonymous(string CustomerId);

        Task<Customer> GetLatestCustomer();

        Task<Customer> GetCurrentCustomer(string CustomerId);
    }
}