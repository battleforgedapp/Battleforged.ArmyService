using Battleforged.ArmyService.Domain.Entities;
using MediatR;

namespace Battleforged.ArmyService.Application.BattleSizes.Queries.GetBattleSizes; 

public record GetBattleSizesQuery : IRequest<IQueryable<BattleSize>>;