using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using GLS.Platform.u202311077.Assignments.Domain.Model.Aggregates;
using GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GLS.Platform.u202311077.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for the GLS Platform.
///     Responsible for database configuration, entity mapping, and data seeding.
/// </summary>
/// <param name="options">
///     The options for the database context.
/// </param>
/// <author>Ayrton Llanos</author>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.UseSnakeCaseNamingConvention();

        builder.Entity<Device>().HasData(
            new { Id = 1, MissionId = 301, PreferredThrust = 750.0m, CreatedDate = DateTimeOffset.UtcNow },
            new { Id = 2, MissionId = 302, PreferredThrust = 820.5m, CreatedDate = DateTimeOffset.UtcNow },
            new { Id = 3, MissionId = 303, PreferredThrust = 910.0m, CreatedDate = DateTimeOffset.UtcNow },
            new { Id = 4, MissionId = 304, PreferredThrust = 880.5m, CreatedDate = DateTimeOffset.UtcNow }
        );

        builder.Entity<Device>().OwnsOne(d => d.MacAddress, m =>
        {
            m.WithOwner();
            m.Property(p => p.Address).HasColumnName("mac_address");
            m.HasData(
                new { DeviceId = 1, Address = "A1-B2-C3-D4-E5-F6" },
                new { DeviceId = 2, Address = "F6-E5-D4-C3-B2-A1" },
                new { DeviceId = 3, Address = "12-34-56-78-9A-BC" },
                new { DeviceId = 4, Address = "BC-9A-78-56-34-12" }
            );
        });
    }

    public DbSet<Device> Devices { get; set; }
}