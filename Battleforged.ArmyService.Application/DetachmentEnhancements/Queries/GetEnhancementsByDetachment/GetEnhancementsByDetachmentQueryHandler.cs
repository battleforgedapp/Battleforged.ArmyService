using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.DetachmentEnhancements.Queries.GetEnhancementsByDetachment; 

public class GetEnhancementsByDetachmentQueryHandler(IDetachmentEnhancementRepository repo)
    : IRequestHandler<GetEnhancementsByDetachmentQuery, IQueryable<DetachmentEnhancement>> {
    
    public async Task<IQueryable<DetachmentEnhancement>> Handle(GetEnhancementsByDetachmentQuery request, CancellationToken cancellationToken)
        => await Task.Run(() => repo
            .AsQueryable()
            .Where(x => x.DetachmentId == request.DetachmentId)
            .OrderBy(x => x.PointCost), 
            cancellationToken
        );
}