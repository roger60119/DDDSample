using DDDSample.Domain.Members.Entities;
using DDDSample.Domain.Orders.Entities;
using DDDSample.Domain.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDDSample.Infrastructure.Common;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Member> Members { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.Property<DateTime>("UpdatedDate").HasDefaultValueSql("SYSDATETIME()");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property<DateTime>("UpdatedDate").HasDefaultValueSql("SYSDATETIME()");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);

            entity.HasOne<Order>()
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderId);

            entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
        {
            entry.Property("UpdatedDate").CurrentValue = DateTime.UtcNow;
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
