namespace Restaurant.System.Models.Dtos
{
    public class AddressDto
    {
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // public static string ToAddressString(this AddressDto address)
        // {
        //     return $"{String.Concat(address.Address1 ," ", address.Address2, " ", address.PostalCode, " ", State, " ", address.Country)}";
        // }
    }
}