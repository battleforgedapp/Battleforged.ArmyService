using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.Detachments.Queries.GetDetachmentById; 

public sealed class GetDetachmentByIdQueryHandler(IDetachmentRepository repo)
    : IRequestHandler<GetDetachmentByIdQuery, Detachment?> {

    public async Task<Detachment?> Handle(GetDetachmentByIdQuery request, CancellationToken cancellationToken)
        => await repo.GetByIdAsync(request.DetachmentId, cancellationToken);
}