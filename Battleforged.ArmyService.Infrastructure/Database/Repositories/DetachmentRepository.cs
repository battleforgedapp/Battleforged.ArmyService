using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class DetachmentRepository(AppDbContext ctx) : IDetachmentRepository {

    public async Task<int> AddRangeAsync(IReadOnlyList<Detachment> entities, CancellationToken ct = default) {
        await ctx.Detachments.AddRangeAsync(entities, ct);
        await ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    public IQueryable<Detachment> AsQueryable() => ctx.Detachments.AsQueryable();

    public async Task<IEnumerable<Detachment>> FetchDetachmentsForArmyAsync(Guid armyId, CancellationToken ct = default) {
        return await ctx.Detachments
            .OrderBy(x => x.DetachmentName)
            .Where(x => x.ArmyId == armyId)
            .ToListAsync(ct);
    }
}