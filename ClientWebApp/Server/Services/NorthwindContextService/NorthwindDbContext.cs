﻿namespace ClientWebApp.Server.Services.NorthwindContextService
{
    public class NorthwindDbContext : DbContext
    {
        private readonly IDateTime _dateTime;

        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<EmployeeTerritory> EmployeeTerritories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Region> Region { get; set; } = null!;
        public DbSet<Shipper> Shippers { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Territory> Territories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "Northwind.db");
            builder.UseSqlite($"FileName={filePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NorthwindDbContext).Assembly);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.CreatedBy = "urgen0240@gmail.com";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedBy = "urgen0240@gmail.com";
                        break;

                    case EntityState.Deleted:
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedBy = "urgen0240@gmail.com";
                        break;

                    case EntityState.Detached:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;

                    case EntityState.Unchanged:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

