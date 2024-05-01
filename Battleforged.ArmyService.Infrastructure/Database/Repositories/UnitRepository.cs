using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Battleforged.ArmyService.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class UnitRepository(AppDbContext ctx) : IUnitRepository {

    public async Task<int> AddRangeAsync(IReadOnlyList<Unit> entities, CancellationToken ct = default) {
        await ctx.Units.AddRangeAsync(entities, ct);
        await ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    public IQueryable<Unit> AsQueryable() => ctx.Units.AsQueryable();

    public async Task<IPagedResult<Guid, Unit>> FetchPagedForArmyAsync(Guid armyId, Guid? cursor, int pageSize, CancellationToken ct = default) {
        var totalCount = await ctx.Units.Where(x => x.ArmyId == armyId).CountAsync(ct);
        var results = await ctx.Units
            .OrderBy(x => x.UnitName)
            .Where(x =>
                x.ArmyId == armyId
                && (cursor == null || x.Id >= cursor.Value)
            )
            .Take(pageSize + 1)
            .ToListAsync(ct);

        return new PagedResult<Guid, Unit>(
            results.Take(pageSize),
            totalCount,
            pageSize,
            results.Count > pageSize ? results.Last().Id : null,
            results.Count > 0 ? results.First().Id : null
        );
    }
}