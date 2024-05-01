using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Infrastructure.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations; 

public static class BattleSizeEntityConfig {

    public static void RegisterBattleSizeEntity(this ModelBuilder builder) {
        builder.Entity<BattleSize>(cfg => {
            // configure the table properties
            cfg.ToTable("def_battle_sizes");
            cfg.HasKey(pk => pk.Id);
            
            // configure the base data
            cfg.HasData(BattleSizes.Data);
            
            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);
            
            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("battle_size_id")
                .IsRequired();

            cfg.Property(p => p.Description)
                .HasColumnName("battle_size")
                .HasMaxLength(256)
                .IsRequired();

            cfg.Property(p => p.PointLimit)
                .HasColumnName("points_limit")
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