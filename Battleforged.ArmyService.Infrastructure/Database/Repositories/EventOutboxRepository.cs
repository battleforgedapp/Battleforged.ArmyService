using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories;

/// <inheritdoc cref="IEventOutboxRepository" />
public sealed class EventOutboxRepository(IDbContextFactory<AppDbContext> ctx) : IEventOutboxRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public async Task<EventOutbox> AddAsync(EventOutbox entity, CancellationToken ct = default) {
        await _ctx.Outbox.AddAsync(entity, ct);
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }

    public async ValueTask DisposeAsync() {
        await _ctx.DisposeAsync();
    }
}