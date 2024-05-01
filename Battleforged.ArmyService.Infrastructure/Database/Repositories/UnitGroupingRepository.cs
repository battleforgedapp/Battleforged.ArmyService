using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class UnitGroupingRepository(IDbContextFactory<AppDbContext> ctx) : IUnitGroupingRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public async Task<int> AddRangeAsync(IReadOnlyList<UnitGrouping> entities, CancellationToken ct = default) {
        await _ctx.UnitGroupings.AddRangeAsync(entities, ct);
        await _ctx.SaveChangesAsync(ct);
        return entities.Count;
    }
    
    public IQueryable<UnitGrouping> AsQueryable() => _ctx.UnitGroupings.AsQueryable();
    
    public async ValueTask DisposeAsync() {
        await _ctx.DisposeAsync();
    }
}