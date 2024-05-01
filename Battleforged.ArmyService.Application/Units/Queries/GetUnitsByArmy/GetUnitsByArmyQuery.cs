using MediatR;
using Unit = Battleforged.ArmyService.Domain.Entities.Unit;

namespace Battleforged.ArmyService.Application.Units.Queries.GetUnitsByArmy; 

public record GetUnitsByArmyQuery(Guid ArmyId) : IRequest<IQueryable<Unit>>;