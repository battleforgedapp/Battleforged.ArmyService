using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Application.Detachments.Queries.GetDetachmentById; 

public record GetDetachmentByIdQuery(Guid DetachmentId) : IRequest<Detachment?>;