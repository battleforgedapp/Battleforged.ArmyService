using Battleforged.ArmyService.Application.BattleSizes.Queries.GetBattleSizes;
using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Graph.Queries; 

[ExtendObjectType("Query")]
public class BattleSizeQueries {

    [UseSorting]
    public async Task<IQueryable<BattleSize>> GetBattleSizesAsync([Service] ISender mediatr, CancellationToken ct)
        => await mediatr.Send(new GetBattleSizesQuery(), ct);
}