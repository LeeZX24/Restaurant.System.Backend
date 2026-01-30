namespace Restaurant.System.Models.Dtos.Shared
{
    public class AddressDto
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        // public string ToAddressString(this AddressDto address)
        // {
        //     return $"{String.Concat(Address1 ," ", Address2, " ", PostalCode, " ", State, " ", Country)}";
        // }
    }
}