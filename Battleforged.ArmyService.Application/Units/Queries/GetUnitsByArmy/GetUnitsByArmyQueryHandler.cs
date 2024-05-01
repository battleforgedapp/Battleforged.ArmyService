using Battleforged.ArmyService.Domain.Repositories;
using MediatR;
using Unit = Battleforged.ArmyService.Domain.Entities.Unit;

namespace Battleforged.ArmyService.Application.Units.Queries.GetUnitsByArmy; 

public class GetUnitsByArmyQueryHandler(IUnitRepository repo) : IRequestHandler<GetUnitsByArmyQuery, IQueryable<Unit>> {

    public async Task<IQueryable<Unit>> Handle(GetUnitsByArmyQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo
            .AsQueryable()
            .Where(x => x.ArmyId == request.ArmyId)
            .OrderBy(x => x.UnitName), 
            cancellationToken
        );
}