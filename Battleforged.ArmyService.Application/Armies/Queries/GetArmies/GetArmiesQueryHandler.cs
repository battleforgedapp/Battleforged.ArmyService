using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.Armies.Queries.GetArmies; 

public class GetArmiesQueryHandler(IArmyRepository repo) : IRequestHandler<GetArmiesQuery, IQueryable<Army>> {

    public async Task<IQueryable<Army>> Handle(GetArmiesQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo.AsQueryable().OrderBy(x => x.Name), cancellationToken);
}