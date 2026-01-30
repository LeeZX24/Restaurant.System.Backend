namespace Restaurant.System.Models.Enums
{
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed
    }

    public enum MenuDays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
        AllDays,
    }
}
