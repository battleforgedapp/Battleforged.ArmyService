using Battleforged.ArmyService.Application.UnitGroupings.Queries.GetGroupingsByUnit;
using Battleforged.ArmyService.Domain.Entities;
using MediatR;
using Unit = Battleforged.ArmyService.Domain.Entities.Unit;

namespace Battleforged.ArmyService.Graph.Nodes; 

[Node]
[ExtendObjectType(typeof(Unit))]
public class UnitNodes {

    [UseSorting]
    public async Task<IQueryable<UnitGrouping>> GetGroupingsAsync(
        [Parent] Unit unit,
        [Service] IMediator mediatr,
        CancellationToken ct
    ) => await mediatr.Send(new GetGroupingsByUnitQuery(unit.Id), ct);
}