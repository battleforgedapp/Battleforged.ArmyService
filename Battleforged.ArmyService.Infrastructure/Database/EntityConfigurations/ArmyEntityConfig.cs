using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations;

/// <summary>
/// Extension method for building the table structure for our army model
/// </summary>
public static class ArmyEntityConfig {

    public static void RegisterArmyEntity(this ModelBuilder builder) {
        builder.Entity<Army>(cfg => {
            // configure the base table properties
            cfg.ToTable("armies");
            cfg.HasKey(pk => pk.Id);
            
            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("army_id")
                .HasColumnType("char(36)")
                .IsRequired();

            cfg.Property(p => p.ParentArmyId)
                .HasColumnName("parent_army_id")
                .HasColumnType("char(36)")
                .HasDefaultValue(null)
                .IsRequired(false);

            cfg.Property(p => p.Name)
                .HasColumnName("army_name")
                .HasMaxLength(256)
                .IsRequired();

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