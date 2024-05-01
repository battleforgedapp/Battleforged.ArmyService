using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class DetachmentRepository(IDbContextFactory<AppDbContext> ctx) : IDetachmentRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public async Task<int> AddRangeAsync(IReadOnlyList<Detachment> entities, CancellationToken ct = default) {
        await _ctx.Detachments.AddRangeAsync(entities, ct);
        await _ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    public IQueryable<Detachment> AsQueryable() => _ctx.Detachments.AsQueryable();

    public async ValueTask DisposeAsync() {
        await _ctx.DisposeAsync();
    }
    
    public async Task<IEnumerable<Detachment>> FetchDetachmentsForArmyAsync(Guid armyId, CancellationToken ct = default) {
        return await _ctx.Detachments
            .OrderBy(x => x.DetachmentName)
            .Where(x => x.ArmyId == armyId)
            .ToListAsync(ct);
    }

    public async Task<Detachment?> GetByIdAsync(Guid detachmentId, CancellationToken ct = default) {
        return await _ctx.Detachments.FirstOrDefaultAsync(x => x.Id == detachmentId, ct);
    }
}