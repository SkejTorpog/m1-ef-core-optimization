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
            builder.ToTable("courier").HasMany(c => c.Deliveries).WithOne(d => d.Courier);
            builder.Property(c => c.VehicleType).HasConversion<string>();
        }
    }

    public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("delivery").HasIndex(d => d.CourierId);
            builder.Property(d => d.Status).HasConversion<string>();
        }
    }
}
