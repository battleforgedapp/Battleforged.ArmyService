using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations;

/// <summary>
/// Extension method for building the table structure for our ability model
/// </summary>
public static class AbilityEntityConfig {

    public static void RegisterAbilityEntity(this ModelBuilder builder) {
        builder.Entity<Ability>(cfg => {
            // configure the base table properties
            cfg.ToTable("abilities");
            cfg.HasKey(pk => pk.Id);
            
            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("ability_id")
                .HasColumnType("char(36)")
                .IsRequired();

            cfg.Property(p => p.Type)
                .HasColumnName("ability_type")
                .IsRequired();

            cfg.Property(p => p.Name)
                .HasColumnName("ability_name")
                .HasMaxLength(256)
                .IsRequired();

            cfg.Property(p => p.Text)
                .HasColumnName("ability_text")
                .HasColumnType("mediumtext")
                .IsRequired(false);
            
            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date_utc")
                .HasColumnType("datetime(6)")
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            cfg.Property(p => p.DeletedDate)
                .HasColumnName("deleted_date_utc")
                .HasColumnType("datetime(6)")
                .HasDefaultValue(null)
                .IsRequired(false);
            
            // configure how the base query run
            // this by default will filter out any entities that have been "deleted"
            cfg.HasQueryFilter(x => x.DeletedDate == null);
        });
    }
}