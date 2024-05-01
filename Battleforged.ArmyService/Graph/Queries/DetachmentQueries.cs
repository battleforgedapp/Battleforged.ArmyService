using Battleforged.ArmyService.Application.Detachments.Queries.GetDetachmentById;
using Battleforged.ArmyService.Application.Detachments.Queries.GetDetachmentsByArmy;
using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Graph.Queries; 

[ExtendObjectType("Query")]
public class DetachmentQueries {

    [UseSorting]
    public async Task<IQueryable<Detachment>> GetDetachmentsForArmyAsync([Service] ISender mediatr, Guid armyId, CancellationToken ct)
        => await mediatr.Send(new GetDetachmentsByArmyQuery(armyId), ct);

    public async Task<Detachment?> GetDetachmentByIdAsync([Service] IMediator mediatr, Guid id, CancellationToken ct)
        => await mediatr.Send(new GetDetachmentByIdQuery(id), ct);
}