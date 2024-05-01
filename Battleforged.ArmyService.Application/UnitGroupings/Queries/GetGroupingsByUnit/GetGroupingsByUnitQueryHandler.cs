using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.UnitGroupings.Queries.GetGroupingsByUnit; 

public class GetGroupingsByUnitQueryHandler(IUnitGroupingRepository repo) 
    : IRequestHandler<GetGroupingsByUnitQuery, IQueryable<UnitGrouping>> {

    public async Task<IQueryable<UnitGrouping>> Handle(GetGroupingsByUnitQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo
                .AsQueryable()
                .Where(x => x.UnitId == request.UnitId)
                .OrderBy(x => x.PointCost),
            cancellationToken
        );
}