using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations; 

public static class UnitGroupingEntityConfig {

    public static void RegisterUnitGroupingEntity(this ModelBuilder builder) {
        builder.Entity<UnitGrouping>(cfg => {
            // configure the table properties
            cfg.ToTable("unit_groupings");
            cfg.HasKey(pk => pk.Id);

            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("unit_grouping_id")
                .IsRequired();
            
            cfg.Property(p => p.UnitId)
                .HasColumnName("unit_id")
                .IsRequired();
            
            cfg.Property(p => p.ModelCount)
                .HasColumnName("model_count")
                .IsRequired();
            
            cfg.Property(p => p.PointCost)
                .HasColumnName("points_cost")
                .IsRequired();
            
            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            cfg.Property(p => p.DeletedDate)
                .HasColumnName("deleted_date")
                .IsRequired(false);
        });
    }
}