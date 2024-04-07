using Battleforged.ArmyService.Domain.Enums;

namespace Battleforged.ArmyService.Domain.Entities;

/// <summary>
/// Represents an ability that a unit or weapon can be assigned.
/// </summary>
public sealed class Ability {
    
    /// <summary>
    /// The unique identifier for the ability within the system.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// The type of ability. Who and what it can be assigned too!
    /// </summary>
    public AbilityTypes Type { get; set; }

    /// <summary>
    /// The name of the ability.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The text description of what the ability does.
    /// </summary>
    public string Text { get; set; } = string.Empty;
    
    /// <summary>
    /// The date that the ability/domain model was created.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// The date that the ability was removed. Will removed/filter out
    /// from results in our presentation layer.
    /// </summary>
    public DateTime? DeletedDate { get; set; }
}