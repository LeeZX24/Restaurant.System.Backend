namespace Restaurant.System.Models.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressGuid { get; set; }
        public required string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public required string State { get; set; }
        public string Country { get; set; }
        public bool IsDefaultAddress { get; set; }
    }
}