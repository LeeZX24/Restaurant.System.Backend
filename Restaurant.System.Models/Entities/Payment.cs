namespace Restaurant.System.Models.Entities
{
    public class Payment
    {
        public int Id { get; set; } // Primary Key
        public required string PaymentId { get; set; }
        public required string OrderNumber { get; set; }
        public required string PaymentCode { get; set; }
        public required string PaymentMethod { get; set; }
        public required string PaymentStatus { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMessage { get; set; }
        public DateTime PaymentDateTime { get; set; }
        public Order Order { get; set; } // Foreign Key
    }
}
