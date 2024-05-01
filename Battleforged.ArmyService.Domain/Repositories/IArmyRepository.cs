using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories;

public interface IArmyRepository {

    Task<Army> AddAsync(Army entity, CancellationToken ct = default);

    Task<int> AddRangeAsync(IReadOnlyList<Army> entities, CancellationToken ct = default);

    IQueryable<Army> AsQueryable();
    
    void Delete(Army entity);

    Task<IPagedResult<Guid, Army>> FetchPagedAsync(Guid? cursor, int pageSize, CancellationToken ct = default);

    Task<Army?> GetByIdAsync(Guid armyId, CancellationToken ct = default);
}