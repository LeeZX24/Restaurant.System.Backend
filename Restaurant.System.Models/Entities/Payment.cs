namespace Restaurant.System.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; } // Primary Key
        public string PaymentCode { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMessage { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}
