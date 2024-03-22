namespace Battleforged.ArmyService.Domain.Events;

public sealed class ArmyDeletedEvent {
    
    public Guid ArmyId { get; set; }
    
    public DateTime DeletedDate { get; set; } = DateTime.UtcNow;
}