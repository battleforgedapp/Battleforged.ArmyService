using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations;
using Battleforged.ArmyService.Infrastructure.Database.Functions;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts) {

    public DbSet<Ability> Abilities { get; set; } = null!;
    
    public DbSet<Army> Armies { get; set; } = null!;

    public DbSet<EventOutbox> Outbox { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder) {
        // register the domain model table structures
        builder.RegisterAbilityEntity();
        builder.RegisterArmyEntity();
        builder.RegisterEventOutboxEntity();
        
        // register the functions that will provide help when querying our data
        GuidFunctions.Register(builder);
        
        // register the entity model relationships
        builder.RegisterRelationships();
        base.OnModelCreating(builder);
    }
}