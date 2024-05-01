using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using Battleforged.ArmyService.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories;

/// <inheritdoc cref="IArmyRepository" />
public sealed class ArmyRepository(IDbContextFactory<AppDbContext> ctx) : IArmyRepository {

    private readonly AppDbContext _ctx = ctx.CreateDbContext();
    
    /// <inheritdoc cref="IArmyRepository.AddAsync" />
    public async Task<Army> AddAsync(Army entity, CancellationToken ct = default) {
        await _ctx.Armies.AddAsync(entity, ct);
        await _ctx.SaveChangesAsync(ct);
        return entity;
    }

    /// <inheritdoc cref="IArmyRepository.AddRangeAsync" />
    public async Task<int> AddRangeAsync(IReadOnlyList<Army> entities, CancellationToken ct = default) {
        await _ctx.Armies.AddRangeAsync(entities, ct);
        await _ctx.SaveChangesAsync(ct);
        return entities.Count;
    }

    /// <inheritdoc cref="IArmyRepository.AsQueryable" />
    public IQueryable<Army> AsQueryable() => _ctx.Armies.AsQueryable();
    
    /// <inheritdoc cref="IArmyRepository.Delete" />
    public void Delete(Army entity) {
        entity.DeletedDate = DateTime.UtcNow;
        _ctx.Armies.Update(entity);
    }
    
    /// <inheritdoc cref="IArmyRepository.DisposeAsync" />
    public async ValueTask DisposeAsync() => await _ctx.DisposeAsync();
    
    /// <inheritdoc cref="IArmyRepository.FetchPagedAsync" />
    public async Task<IPagedResult<Guid, Army>> FetchPagedAsync(Guid? cursor, int pageSize, CancellationToken ct = default) {
        var query = _ctx.Armies
            .Where(x => (cursor == null || x.Id >= cursor.Value))
            .OrderBy(x => x.Name);

        var totalCount = await query.CountAsync(ct);
        var results = await query.Take(pageSize + 1).ToListAsync(ct);

        return new PagedResult<Guid, Army>(
            results.Take(pageSize),
            totalCount,
            pageSize,
            results.Count > pageSize ? results.Last().Id : null,
            results.Count > 0 ? results.First().Id : null
        );
    }

    /// <inheritdoc cref="IArmyRepository.GetByIdAsync" />
    public async Task<Army?> GetByIdAsync(Guid armyId, CancellationToken ct = default) {
        return await _ctx.Armies.FirstOrDefaultAsync(x => x.Id == armyId, ct);
    }

    
}