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

        // Menu
        public DbSet<Menu> Menus => Set<Menu>();
        public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<MenuSchedule> MenuSchedules => Set<MenuSchedule>();
        public DbSet<MenuSection> MenuSections => Set<MenuSection>();

        //Order
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<CustomerOrder> CustomerOrders => Set<CustomerOrder>();
        public DbSet<OrderGroup> OrderGroups => Set<OrderGroup>();

        //Payment
        public DbSet<Payment> Payments => Set<Payment>();

        //System Related
        public DbSet<Dropdown> Dropdowns => Set<Dropdown>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Handle Existing Table
            // User
            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Member>().ToTable("Members");
            builder.Entity<Staff>().ToTable("Staffs");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<StaffRoles>().ToTable("StaffRoles");
            builder.Entity<User>().ToTable("Users");
            
            // Menu
            builder.Entity<MenuItem>().ToTable("MenuItems");
            builder.Entity<MenuCategory>().ToTable("MenuCategorys");
            builder.Entity<MenuSchedule>().ToTable("MenuSchedules");
            builder.Entity<Menu>().ToTable("Menu");

            builder.Entity<MenuSection>().ToTable("MenuSections");

            // Order
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<OrderItem>().ToTable("OrderItems");
            builder.Entity<CustomerOrder>().ToTable("CustomerOrders");
            builder.Entity<OrderGroup>().ToTable("OrderGroups");

            //Payment
            builder.Entity<Payment>().ToTable("Payments");
            
            // System Related
            builder.Entity<Dropdown>().ToTable("Dropdowns");
            #endregion

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
                    a.ToTable("Address");
                    a.WithOwner().HasForeignKey("MemberId");
                    a.HasKey(x => x.AddressGuid);
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
                .OwnsOne(o => o.DeliveryAddress, a =>
                {
                    a.ToTable("OrderAddress");
                    a.WithOwner().HasForeignKey("OrderId");
                    a.HasKey(x => x.AddressGuid);
                });
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
