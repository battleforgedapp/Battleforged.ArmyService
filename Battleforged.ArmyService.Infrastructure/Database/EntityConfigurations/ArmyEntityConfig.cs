using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations;

/// <summary>
/// Extension method for building the table structure for our army model
/// </summary>
public static class ArmyEntityConfig {

    public static void RegisterArmyEntity(this ModelBuilder builder) {
        builder.Entity<Army>(cfg => {
            // configure the table properties
            cfg.ToTable("armies");
            cfg.HasKey(pk => pk.Id);

            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("army_id")
                .IsRequired();
            
            cfg.Property(p => p.ParentArmyId)
                .HasColumnName("army_parent_id")
                .IsRequired(false);

            cfg.Property(p => p.Name)
                .HasColumnName("army_name")
                .HasMaxLength(256)
                .IsRequired();

            cfg.Property(p => p.Type)
                .HasColumnName("army_type")
                .IsRequired();

            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            cfg.Property(p => p.DeletedDate)
                .HasColumnName("deleted_date")
                .IsRequired(false);
            
            // configure relationships
            cfg.HasOne<Army>()
                .WithMany()
                .HasPrincipalKey(pk => pk.Id)
                .HasForeignKey(fk => fk.ParentArmyId)
                .IsRequired(false);
            
            cfg.HasMany<Unit>()
                .WithOne()
                .HasPrincipalKey(pk => pk.Id)
                .HasForeignKey(fk => fk.ArmyId);

            cfg.HasMany<Detachment>()
                .WithOne()
                .HasPrincipalKey(pk => pk.Id)
                .HasForeignKey(fk => fk.ArmyId);
        });
    }
}