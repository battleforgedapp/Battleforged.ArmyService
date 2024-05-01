using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories; 

public sealed class BattleSizeRepository(IDbContextFactory<AppDbContext> ctx) : IBattleSizeRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    public IQueryable<BattleSize> AsQueryable() 
        => _ctx.BattleSizes.AsQueryable();
    
    public async ValueTask DisposeAsync()
        => await _ctx.DisposeAsync();
    
    public async Task<IEnumerable<BattleSize>> FetchAsync(CancellationToken ct = default)
        => await _ctx.BattleSizes.ToListAsync(ct);
}