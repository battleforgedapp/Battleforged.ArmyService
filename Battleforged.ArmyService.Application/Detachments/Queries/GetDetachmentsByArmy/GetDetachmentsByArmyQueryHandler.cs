using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.Detachments.Queries.GetDetachmentsByArmy; 

public class GetDetachmentsByArmyQueryHandler(IDetachmentRepository repo) 
    : IRequestHandler<GetDetachmentsByArmyQuery, IQueryable<Detachment>> {

    public async Task<IQueryable<Detachment>> Handle(GetDetachmentsByArmyQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo
            .AsQueryable()
            .Where(x => x.ArmyId == request.ArmyId)
            .OrderBy(x => x.DetachmentName), 
            cancellationToken
        );
}