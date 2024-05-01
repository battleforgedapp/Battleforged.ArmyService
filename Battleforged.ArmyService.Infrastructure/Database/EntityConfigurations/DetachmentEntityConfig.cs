using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations; 

public static class DetachmentEntityConfig {

    public static void RegisterDetachmentEntity(this ModelBuilder builder) {
        builder.Entity<Detachment>(cfg => {
            // configure the table properties
            cfg.ToTable("detachments");
            cfg.HasKey(pk => pk.Id);

            // configure the query filters
            cfg.HasQueryFilter(q => q.DeletedDate == null);

            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("detachment_id")
                .IsRequired();
            
            cfg.Property(p => p.ArmyId)
                .HasColumnName("army_id")
                .IsRequired();

            cfg.Property(p => p.DetachmentName)
                .HasColumnName("detachment_name")
                .HasMaxLength(256)
                .IsRequired();
            
            cfg.Property(p => p.RuleName)
                .HasColumnName("rule_name")
                .HasMaxLength(256)
                .IsRequired();
            
            cfg.Property(p => p.RuleText)
                .HasColumnName("rule_text")
                .HasColumnType("text")
                .IsRequired(false);

            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            cfg.Property(p => p.DeletedDate)
                .HasColumnName("deleted_date")
                .IsRequired(false);
            
            // configure relationships
            cfg.HasMany<DetachmentEnhancement>()
                .WithOne()
                .HasPrincipalKey(pk => pk.Id)
                .HasForeignKey(fk => fk.DetachmentId);
        });
    }
}