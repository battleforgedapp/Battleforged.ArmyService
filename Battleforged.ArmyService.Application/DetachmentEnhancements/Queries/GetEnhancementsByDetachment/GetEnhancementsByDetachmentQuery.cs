using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Application.DetachmentEnhancements.Queries.GetEnhancementsByDetachment; 

public record GetEnhancementsByDetachmentQuery(Guid DetachmentId) : IRequest<IQueryable<DetachmentEnhancement>>;