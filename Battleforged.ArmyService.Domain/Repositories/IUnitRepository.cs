using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories; 

public interface IUnitRepository : IAsyncDisposable {
    Task<int> AddRangeAsync(IReadOnlyList<Unit> entities, CancellationToken ct = default);
    IQueryable<Unit> AsQueryable();
    Task<IPagedResult<Guid, Unit>> FetchPagedForArmyAsync(Guid armyId, Guid? cursor, int pageSize, CancellationToken ct = default);
}