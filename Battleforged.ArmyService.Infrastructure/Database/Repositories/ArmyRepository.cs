using Battleforged.ArmyService.Domain.Repositories;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories;

/// <inheritdoc cref="IArmyRepository" />
public sealed class ArmyRepository(AppDbContext ctx) : IArmyRepository {
    
}