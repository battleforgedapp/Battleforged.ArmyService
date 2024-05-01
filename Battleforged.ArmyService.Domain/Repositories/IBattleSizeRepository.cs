using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories; 

/// <summary>
/// Provides a way to interact with the battle size definition domain model in the data store.
/// </summary>
public interface IBattleSizeRepository : IAsyncDisposable {
    IQueryable<BattleSize> AsQueryable();
    Task<IEnumerable<BattleSize>> FetchAsync(CancellationToken ct = default);
}