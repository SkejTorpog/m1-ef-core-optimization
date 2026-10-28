using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace m1_ef_core_optimization.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Courier> Couriers { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourierConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
    }

    public class CourierConfiguration : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.ToTable("courier");
            builder
                .HasMany(c => c.Deliveries)
                .WithOne(d => d.Courier)
                .HasForeignKey(d => d.CourierId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(d => d.VehicleType).IsRequired();
        }
    }

    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("delivery");
            builder
                .HasIndex(d => new { d.CourierId, d.Status })
                .HasDatabaseName("ix_delivery_courier_id_status");
            builder.Property(d => d.Status).IsRequired();
        }
    }
}
