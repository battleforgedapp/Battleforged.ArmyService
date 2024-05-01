using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Application.Armies.Queries.GetArmies; 

public record GetArmiesQuery() : IRequest<IQueryable<Army>>;