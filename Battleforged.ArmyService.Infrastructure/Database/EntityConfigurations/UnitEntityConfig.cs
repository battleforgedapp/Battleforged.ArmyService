using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations; 

public static class UnitEntityConfig {

    public static void RegisterUnitEntity(this ModelBuilder builder) {
        builder.Entity<Unit>(cfg => {
            // configure the table properties
            cfg.ToTable("units");
            cfg.HasKey(pk => pk.Id);

            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("unit_id")
                .IsRequired();
            
            cfg.Property(p => p.ArmyId)
                .HasColumnName("army_id")
                .IsRequired();

            cfg.Property(p => p.UnitName)
                .HasColumnName("unit_name")
                .HasMaxLength(256)
                .IsRequired();

            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            cfg.Property(p => p.DeletedDate)
                .HasColumnName("deleted_date")
                .IsRequired(false);
            
            // configure relationships
            cfg.HasMany<UnitGrouping>()
                .WithOne()
                .HasPrincipalKey(pk => pk.Id)
                .HasForeignKey(fk => fk.UnitId);
        });
    }
}