using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories; 

public interface IDetachmentRepository {
    Task<int> AddRangeAsync(IReadOnlyList<Detachment> entities, CancellationToken ct = default);
    IQueryable<Detachment> AsQueryable();
    Task<IEnumerable<Detachment>> FetchDetachmentsForArmyAsync(Guid armyId, CancellationToken ct = default);
}