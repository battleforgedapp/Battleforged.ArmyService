using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class UnitGroupingRepository(AppDbContext ctx) : IUnitGroupingRepository {

    public async Task<int> AddRangeAsync(IReadOnlyList<UnitGrouping> entities, CancellationToken ct = default) {
        await ctx.UnitGroupings.AddRangeAsync(entities, ct);
        await ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    public IQueryable<UnitGrouping> AsQueryable() => ctx.UnitGroupings.AsQueryable();
}