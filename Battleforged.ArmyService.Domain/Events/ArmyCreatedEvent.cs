namespace Battleforged.ArmyService.Domain.Events;

public sealed class ArmyCreatedEvent {
    
    public Guid ArmyId { get; set; }
    
    public Guid? ParentArmyId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}