namespace Battleforged.ArmyService.Domain.Entities; 

public sealed class Unit {
    
    public Guid Id { get; set; }
    
    public Guid ArmyId { get; set; }

    public string UnitName { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedDate { get; set; }
}