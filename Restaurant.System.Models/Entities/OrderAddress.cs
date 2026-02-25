using Microsoft.EntityFrameworkCore;

namespace Restaurant.System.Models.Entities
{
    [Owned]
    public class OrderAddress
    {
        public int Id { get; set; } // Primary Key
        public required string Address1 { get; set; }
        public string Address2 { get; set; }
        public required string PostalCode { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public Guid AddressGuid { get; set; } = Guid.CreateVersion7();
    }
}