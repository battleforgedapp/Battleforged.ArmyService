using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Domain.Repositories; 

public interface IDetachmentEnhancementRepository {
    Task<int> AddRangeAsync(IReadOnlyList<DetachmentEnhancement> entities, CancellationToken ct = default);
    IQueryable<DetachmentEnhancement> AsQueryable();
    Task<IEnumerable<DetachmentEnhancement>> FetchEnhancementsForDetachmentAsync(Guid detachmentId, CancellationToken ct = default);
}