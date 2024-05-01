using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Application.Detachments.Queries.GetDetachmentsByArmy; 

public record GetDetachmentsByArmyQuery(Guid ArmyId) : IRequest<IQueryable<Detachment>>;