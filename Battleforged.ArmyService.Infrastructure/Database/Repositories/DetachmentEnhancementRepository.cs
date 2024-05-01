using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class DetachmentEnhancementRepository(AppDbContext ctx) : IDetachmentEnhancementRepository {

    public async Task<int> AddRangeAsync(IReadOnlyList<DetachmentEnhancement> entities, CancellationToken ct = default) {
        await ctx.DetachmentEnhancements.AddRangeAsync(entities, ct);
        await ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    public IQueryable<DetachmentEnhancement> AsQueryable() => ctx.DetachmentEnhancements.AsQueryable();
    
    public async Task<IEnumerable<DetachmentEnhancement>> FetchEnhancementsForDetachmentAsync(Guid detachmentId, CancellationToken ct = default) {
        return await ctx.DetachmentEnhancements
            .OrderBy(x => x.EnhancementName)
            .Where(x => x.DetachmentId == detachmentId)
            .ToListAsync(ct);
    }
}