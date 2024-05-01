using Battleforged.ArmyService.Domain.Enums;

namespace Battleforged.ArmyService.Domain.Entities;

/// <summary>
/// Represents the very top level of a 40k army and shows the base army data, such as the name.
/// </summary>
public sealed class Army {
    
    /// <summary>
    /// The unique identifier for the army within the entire system
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// References another armies unique identifier and tells the system that the base units from that
    /// army are also applied to this army. Primary example would be Blood Angels are also Space Marines.
    /// </summary>
    public Guid? ParentArmyId { get; set; } = null;
    
    /// <summary>
    /// The name of the army to display.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The category/type of army it is. Xenos, Imperium, Chaos, etc.
    /// </summary>
    public ArmyTypes Type { get; set; }
    
    /// <summary>
    /// The date that the domain record was created. Mostly for internal use.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// The date that the record was removed from the system. Used internally and to filter out
    /// anything "old".
    /// </summary>
    public DateTime? DeletedDate { get; set; }
}