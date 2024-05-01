using Battleforged.ArmyService.Application.Units.Queries.GetUnitsByArmy;
using MediatR;
using Unit = Battleforged.ArmyService.Domain.Entities.Unit;

namespace Battleforged.ArmyService.Graph.Queries; 

[ExtendObjectType("Query")]
public class UnitQueries {

    [UsePaging(IncludeTotalCount = true)]
    [UseSorting]
    public async Task<IQueryable<Unit>> GetUnitsForArmyAsync([Service] ISender mediatr, Guid armyId, CancellationToken ct)
        => await mediatr.Send(new GetUnitsByArmyQuery(armyId), ct);
}