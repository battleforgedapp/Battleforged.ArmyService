using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.BattleSizes.Queries.GetBattleSizes; 

public class GetBattleSizesQueryHandler(IBattleSizeRepository repo) 
    : IRequestHandler<GetBattleSizesQuery, IQueryable<BattleSize>> {

    public async Task<IQueryable<BattleSize>> Handle(GetBattleSizesQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo.AsQueryable().OrderBy(x => x.PointLimit), cancellationToken);
}