using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations; 

public static class DetachmentEnhancementEntityConfig {

    public static void RegisterDetachmentEnhancementEntity(this ModelBuilder builder) {
        builder.Entity<DetachmentEnhancement>(cfg => {
            // configure the table properties
            cfg.ToTable("detachment_enhancements");
            cfg.HasKey(pk => pk.Id);

            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("enhancement_id")
                .IsRequired();
            
            cfg.Property(p => p.DetachmentId)
                .HasColumnName("detachment_id")
                .IsRequired();

            cfg.Property(p => p.EnhancementName)
                .HasColumnName("enhancement_name")
                .HasMaxLength(256)
                .IsRequired();
            
            cfg.Property(p => p.EnhancementText)
                .HasColumnName("enhancement_text")
                .HasColumnType("text")
                .IsRequired(false);
            
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