using Battleforged.ArmyService.Application.DetachmentEnhancements.Queries.GetEnhancementsByDetachment;
using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Graph.Nodes; 

[Node]
[ExtendObjectType(typeof(Detachment))]
public class DetachmentNodes {

    [UseSorting]
    public async Task<IQueryable<DetachmentEnhancement>> GetEnhancementsAsync(
        [Parent] Detachment detachment,
        [Service] IMediator mediatr,
        CancellationToken ct
    ) => await mediatr.Send(new GetEnhancementsByDetachmentQuery(detachment.Id), ct);
}