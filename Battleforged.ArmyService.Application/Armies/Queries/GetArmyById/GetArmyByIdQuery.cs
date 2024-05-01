using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.Armies.Queries.GetArmyById; 

public record GetArmyByIdQuery(Guid Id) : IRequest<Army?>;