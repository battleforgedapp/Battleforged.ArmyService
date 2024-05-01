using Battleforged.ArmyService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.EntityConfigurations;

/// <summary>
/// Extension method for building the table structure for our event outbox model
/// </summary>
public static class EventOutboxEntityConfig {

    public static void RegisterEventOutboxEntity(this ModelBuilder builder) {
        builder.Entity<EventOutbox>(cfg => {
            // configure the base table properties
            cfg.ToTable("event_outbox");
            cfg.HasKey(pk => pk.Id);
            cfg.HasIndex(i => i.SentDate);
            
            // configure the columns
            cfg.Property(p => p.Id)
                .HasColumnName("outbox_id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            cfg.Property(p => p.EventName)
                .HasColumnName("event_name")
                .HasMaxLength(4098)
                .IsRequired();

            cfg.Property(p => p.EventData)
                .HasColumnName("event_data")
                .HasColumnType("text")
                .IsRequired();

            cfg.Property(p => p.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            cfg.Property(p => p.SentDate)
                .HasColumnName("sent_date")
                .HasDefaultValue(null)
                .IsRequired(false);

            cfg.Property(p => p.TotalAttempts)
                .HasColumnName("total_attempts")
                .HasDefaultValue(0)
                .IsRequired();
        });
    }
}