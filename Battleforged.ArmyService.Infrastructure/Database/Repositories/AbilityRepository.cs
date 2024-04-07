using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;

namespace Battleforged.ArmyService.Infrastructure.Database.Repositories;

/// <inheritdoc cref="IAbilityRepository" />
public sealed class AbilityRepository(AppDbContext ctx) : IAbilityRepository {

    /// <inheritdoc cref="IAbilityRepository.AddAsync" />
    public async Task<Ability> AddAsync(Ability entity, CancellationToken ct = default) {
        await ctx.Abilities.AddAsync(entity, ct);
        await ctx.SaveChangesAsync(ct);
        return entity;
    }

    /// <inheritdoc cref="IAbilityRepository.AddRangeAsync" />
    public async Task AddRangeAsync(IEnumerable<Ability> entities, CancellationToken ct = default) {
        await ctx.Abilities.AddRangeAsync(entities, ct);
        await ctx.SaveChangesAsync(ct);
    }
}