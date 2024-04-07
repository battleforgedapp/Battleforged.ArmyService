using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories;

public interface IAbilityRepository {

    Task<Ability> AddAsync(Ability entity, CancellationToken ct = default);

    Task AddRangeAsync(IEnumerable<Ability> entities, CancellationToken ct = default);
}