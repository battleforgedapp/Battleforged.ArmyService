using Battleforged.ArmyService.Application.Armies.Queries.GetArmies;
using Battleforged.ArmyService.Application.Armies.Queries.GetArmyById;
using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Graph.Queries; 

[ExtendObjectType("Query")]
public class ArmyQueries {

    [UsePaging(IncludeTotalCount = true)]
    [UseSorting]
    public async Task<IQueryable<Army>> GetArmiesAsync([Service] ISender mediatr, CancellationToken ct)
        => await mediatr.Send(new GetArmiesQuery(), ct);

    public async Task<Army?> GetArmyByIdAsync([Service] ISender mediatr, Guid id, CancellationToken ct)
        => await mediatr.Send(new GetArmyByIdQuery(id), ct);
}