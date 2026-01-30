using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomer(string CustomerId);
    }
}