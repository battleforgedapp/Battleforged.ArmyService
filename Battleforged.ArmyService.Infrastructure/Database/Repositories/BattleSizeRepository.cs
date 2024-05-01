using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class BattleSizeRepository(AppDbContext ctx) : IBattleSizeRepository {

    public IQueryable<BattleSize> AsQueryable() => ctx.BattleSizes.AsQueryable();
    
    public async Task<IEnumerable<BattleSize>> FetchAsync(CancellationToken ct = default)
        => await ctx.BattleSizes.ToListAsync(ct);
}