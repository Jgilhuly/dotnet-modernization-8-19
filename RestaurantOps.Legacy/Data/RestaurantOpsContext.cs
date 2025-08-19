using Microsoft.EntityFrameworkCore;
using RestaurantOps.Legacy.Models;

namespace RestaurantOps.Legacy.Data
{
    public class RestaurantOpsContext : DbContext
    {
        public RestaurantOpsContext(DbContextOptions<RestaurantOpsContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<RestaurantTable> RestaurantTables => Set<RestaurantTable>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<InventoryTx> InventoryTx => Set<InventoryTx>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Shift> Shifts => Set<Shift>();
        public DbSet<TimeOff> TimeOff => Set<TimeOff>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Categories
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(c => c.CategoryId);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Description).HasMaxLength(500);
            });

            // MenuItems
            modelBuilder.Entity<MenuItem>(e =>
            {
                e.HasKey(m => m.MenuItemId);
                e.Property(m => m.Name).IsRequired();
                e.Property(m => m.Price).HasColumnType("decimal(10,2)");
                e.HasOne<Category>()
                    .WithMany()
                    .HasForeignKey(m => m.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // RestaurantTables
            modelBuilder.Entity<RestaurantTable>(e =>
            {
                e.HasKey(t => t.TableId);
                e.Property(t => t.Name).IsRequired();
            });

            // Orders
            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(o => o.OrderId);
                e.Property(o => o.Status).HasMaxLength(20).HasDefaultValue("Open");
                e.Property(o => o.CreatedAt).HasDefaultValueSql("SYSUTCDATETIME()");
                e.HasMany<OrderLine>()
                    .WithOne()
                    .HasForeignKey(ol => ol.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // OrderLines
            modelBuilder.Entity<OrderLine>(e =>
            {
                e.HasKey(ol => ol.OrderLineId);
                e.Property(ol => ol.PriceEach).HasColumnType("decimal(10,2)");
                e.HasOne<MenuItem>()
                    .WithMany()
                    .HasForeignKey(ol => ol.MenuItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Ingredients
            modelBuilder.Entity<Ingredient>(e =>
            {
                e.HasKey(i => i.IngredientId);
                e.Property(i => i.Name).IsRequired().HasMaxLength(100);
                e.Property(i => i.Unit).IsRequired().HasMaxLength(20);
                e.Property(i => i.QuantityOnHand).HasColumnType("decimal(10,2)");
                e.Property(i => i.ReorderThreshold).HasColumnType("decimal(10,2)");
            });

            // InventoryTx
            modelBuilder.Entity<InventoryTx>(e =>
            {
                e.HasKey(tx => tx.TxId);
                e.Property(tx => tx.QuantityChange).HasColumnType("decimal(10,2)");
                e.Property(tx => tx.Notes).HasMaxLength(255);
                e.HasOne<Ingredient>()
                    .WithMany()
                    .HasForeignKey(tx => tx.IngredientId)
                    .OnDelete(DeleteBehavior.Restrict);
                e.Property(tx => tx.TxDate).HasDefaultValueSql("SYSUTCDATETIME()");
            });

            // Employees
            modelBuilder.Entity<Employee>(e =>
            {
                e.HasKey(emp => emp.EmployeeId);
                e.Property(emp => emp.FirstName).IsRequired().HasMaxLength(50);
                e.Property(emp => emp.LastName).IsRequired().HasMaxLength(50);
                e.Property(emp => emp.Role).IsRequired().HasMaxLength(30);
                e.Property(emp => emp.IsActive).HasDefaultValue(true);
                e.Property(emp => emp.HireDate).HasColumnType("date").HasDefaultValueSql("CAST(GETDATE() AS DATE)");
            });

            // Shifts
            modelBuilder.Entity<Shift>(e =>
            {
                e.HasKey(s => s.ShiftId);
                e.HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey(s => s.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.Property(s => s.ShiftDate).HasColumnType("date");
                e.Property(s => s.StartTime).HasColumnType("time(0)");
                e.Property(s => s.EndTime).HasColumnType("time(0)");
                e.Property(s => s.Role).IsRequired().HasMaxLength(30);
            });

            // TimeOff
            modelBuilder.Entity<TimeOff>(e =>
            {
                e.HasKey(t => t.TimeOffId);
                e.HasOne<Employee>()
                    .WithMany()
                    .HasForeignKey(t => t.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.Property(t => t.StartDate).HasColumnType("date");
                e.Property(t => t.EndDate).HasColumnType("date");
                e.Property(t => t.Status).HasMaxLength(20).HasDefaultValue("Pending");
            });
        }
    }
}


