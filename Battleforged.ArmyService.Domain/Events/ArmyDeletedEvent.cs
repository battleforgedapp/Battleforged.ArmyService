namespace Battleforged.ArmyService.Domain.Events;

public sealed class ArmyDeletedEvent {

    public const string ArmyDeletedEventRoute = "armies.deleted";
    
    public Guid ArmyId { get; set; }
    
    public DateTime DeletedDate { get; set; } = DateTime.UtcNow;
}