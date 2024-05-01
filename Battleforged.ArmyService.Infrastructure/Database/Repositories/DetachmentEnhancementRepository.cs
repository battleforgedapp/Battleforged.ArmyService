using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class DetachmentEnhancementRepository(IDbContextFactory<AppDbContext> ctx) : IDetachmentEnhancementRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public async Task<int> AddRangeAsync(IReadOnlyList<DetachmentEnhancement> entities, CancellationToken ct = default) {
        await _ctx.DetachmentEnhancements.AddRangeAsync(entities, ct);
        await _ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    public IQueryable<DetachmentEnhancement> AsQueryable() => _ctx.DetachmentEnhancements.AsQueryable();
    
    public async ValueTask DisposeAsync() {
        await _ctx.DisposeAsync();
    }
    
    public async Task<IEnumerable<DetachmentEnhancement>> FetchEnhancementsForDetachmentAsync(Guid detachmentId, CancellationToken ct = default) {
        return await _ctx.DetachmentEnhancements
            .OrderBy(x => x.EnhancementName)
            .Where(x => x.DetachmentId == detachmentId)
            .ToListAsync(ct);
    }
}