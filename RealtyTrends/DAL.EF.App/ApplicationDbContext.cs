using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Filter> Filters { get; set; } = default!;
    public DbSet<FilterType> FilterTypes { get; set; } = default!;
    public DbSet<Property> Properties { get; set; } = default!;
    
    public DbSet<PropertyUpdate> PropertyUpdates { get; set; } = default!;
    public DbSet<PropertyField> PropertyFields { get; set; } = default!;
    public DbSet<PropertyStructure> PropertyStructures { get; set; } = default!;
    public DbSet<PropertyType> PropertyTypes { get; set; } = default!;
    public DbSet<Region> Regions { get; set; } = default!;
    public DbSet<RegionProperty> RegionProperties { get; set; } = default!;
    public DbSet<RegionType> RegionTypes { get; set; } = default!;
    public DbSet<TransactionType> TransactionTypes { get; set; } = default!;
    public DbSet<Trigger> Triggers { get; set; } = default!;
    public DbSet<TriggerFilter> TriggerFilters { get; set; } = default!;
    public DbSet<UserTrigger> UserTriggers { get; set; } = default!;
    public DbSet<WebCrawler> WebCrawlers { get; set; } = default!;
    public DbSet<WebCrawlerParameter> WebCrawlerParameters { get; set; } = default!;
    public DbSet<Website> Websites { get; set; } = default!;
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    
    public DbSet<Snapshot> Snapshots { get; set; } = default!;
    
    public DbSet<VirtualStatisticsData> VirtualStatisticsData { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<VirtualStatisticsData>().HasNoKey();
        
        base.OnModelCreating(builder);
        
        // disable cascade delete
        foreach (var foreignKey in builder.Model
                     .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        // When a UserTrigger is deleted, also delete the related Trigger
        builder.Entity<UserTrigger>()
            .HasOne(ut => ut.Trigger)
            .WithMany(t => t.UserTriggers)
            .HasForeignKey(ut => ut.TriggerId)
            .OnDelete(DeleteBehavior.Cascade);

        // When a Trigger is deleted, also delete the related TriggerFilters
        builder.Entity<Trigger>()
            .HasMany(t => t.TriggerFilters)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    
        // When a TriggerFilter is deleted, also delete the related Filter
        builder.Entity<TriggerFilter>()
            .HasOne(tf => tf.Filter)
            .WithMany(f => f.TriggerFilters)
            .HasForeignKey(tf => tf.FilterId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<TriggerFilter>()
            .HasOne(tf => tf.Trigger)
            .WithMany(t => t.TriggerFilters)
            .HasForeignKey(tf => tf.TriggerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}