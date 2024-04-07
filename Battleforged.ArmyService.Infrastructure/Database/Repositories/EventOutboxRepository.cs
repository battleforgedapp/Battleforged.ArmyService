using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories;

/// <inheritdoc cref="IEventOutboxRepository" />
public sealed class EventOutboxRepository(AppDbContext ctx) : IEventOutboxRepository {

    public async Task<EventOutbox> AddAsync(EventOutbox entity, CancellationToken ct = default) {
        await ctx.Outbox.AddAsync(entity, ct);
        await ctx.SaveChangesAsync(ct);
        return entity;
    }
}