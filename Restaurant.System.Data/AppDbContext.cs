using Microsoft.EntityFrameworkCore;
using Restaurant.System.Models.Entities;

namespace Restaurant.System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        //User
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<StaffRoles> StaffRoles => Set<StaffRoles>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Address> Addresses => Set<Address>();

        // Menu
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();

        //Order
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<CustomerOrder> CustomerOrders => Set<CustomerOrder>();
        public DbSet<OrderGroup> OrderGroups => Set<OrderGroup>();
        public DbSet<OrderAddress> OrderAddresses => Set<OrderAddress>();

        //Payment
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Member
            builder.Entity<Customer>()
                .HasOne(c => c.MemberDetails)
                .WithOne(m => m.Customer)
                .HasForeignKey<Member>(m => m.CustomerId)
                .HasPrincipalKey<Customer>(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Member>()
                .OwnsMany(m => m.AddressList, a =>
                {
                    a.WithOwner().HasForeignKey("MemberId");
                    a.Property<Guid>("AddressGuid");
                    a.HasKey("Id", "AddressGuid");
                });
            #endregion

            #region Staff
            builder.Entity<Staff>()
                .HasMany(s => s.StaffRolesList)
                .WithOne(sr => sr.Staff)
                .HasForeignKey(sr => sr.StaffUsername)
                .HasPrincipalKey(s => s.Username)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>()
                .HasMany(r => r.StaffRolesList)
                .WithOne(sr => sr.Role)
                .HasForeignKey(sr => sr.RoleCode)
                .HasPrincipalKey(s => s.RoleCode)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Order
            builder.Entity<Order>()
                .HasMany(o => o.OrderGroups)
                .WithOne(og => og.Order)
                .HasForeignKey(og => og.OrderNumber)
                .HasPrincipalKey(o => o.OrderNumber)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<OrderItem>()
                .HasOne(oi => oi.OrderGroup)
                .WithMany(og => og.OrderItemList)
                .HasForeignKey(oi => oi.OrderGroupId)
                .HasPrincipalKey(og => og.OrderGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasMany(o => o.CustomerOrders)
                .WithOne(co => co.Order)
                .HasForeignKey(co => co.OrderNumber)
                .HasPrincipalKey(o => o.OrderNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>()
                .HasMany(c => c.CustomerOrders)
                .WithOne(co => co.Customer)
                .HasForeignKey(co => co.CustomerId)
                .HasPrincipalKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderNumber)
                .HasPrincipalKey<Order>(o => o.OrderNumber)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(o => o.Staff)
                .WithMany(s => s.OrderHistory)
                .HasForeignKey(o => o.StaffUsername)
                .HasPrincipalKey(s => s.Username)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Order>()
                .OwnsOne(o => o.DeliveryAddress);

             builder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderNumber)
                .HasPrincipalKey<Order>(o => o.OrderNumber)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Menu
            builder.Entity<MenuSection>()
                .HasOne(ms => ms.Menu)
                .WithMany(m => m.MenuSections)
                .HasForeignKey(ms => ms.MenuCode)
                .HasPrincipalKey(m => m.MenuCode)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MenuSection>()
                .HasOne(ms => ms.Category)
                .WithMany(c => c.MenuSections)
                .HasForeignKey(ms => ms.CategoryCode)
                .HasPrincipalKey(c => c.CategoryCode)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MenuSection>()
                .HasOne(ms => ms.MenuSchedule)
                .WithMany(s => s.MenuSectionList)
                .HasForeignKey(ms => ms.ScheduleCode)
                .HasPrincipalKey(s => s.ScheduleCode)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
