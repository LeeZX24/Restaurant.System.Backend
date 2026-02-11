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

    public enum UserType
    {
        Member,
        Staff
    }

    public enum OrderType
    {
        Delivery,
        Table,
        Takeaway
    }

    public enum Status
    {
        Pending = 0,
        Success = 1,
        Error = 2
    }
}
