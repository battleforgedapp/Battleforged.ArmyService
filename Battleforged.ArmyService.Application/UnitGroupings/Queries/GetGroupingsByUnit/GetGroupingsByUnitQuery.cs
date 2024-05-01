using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Application.UnitGroupings.Queries.GetGroupingsByUnit; 

public record GetGroupingsByUnitQuery(Guid UnitId) : IRequest<IQueryable<UnitGrouping>>;