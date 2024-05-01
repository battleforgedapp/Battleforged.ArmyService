using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories; 

public interface IUnitGroupingRepository : IAsyncDisposable {
    Task<int> AddRangeAsync(IReadOnlyList<UnitGrouping> entities, CancellationToken ct = default);
    IQueryable<UnitGrouping> AsQueryable();
}