using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.Armies.Queries.GetArmyById; 

public sealed class GetArmyByIdQueryHandler(IArmyRepository repo) 
    : IRequestHandler<GetArmyByIdQuery, Army?> {

    public async Task<Army?> Handle(GetArmyByIdQuery request, CancellationToken cancellationToken)
        => await repo.GetByIdAsync(request.Id, cancellationToken);
}