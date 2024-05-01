namespace Battleforged.ArmyService.Domain.Entities; 

public sealed class BattleSize {
    
    public Guid Id { get; set; }

    public string Description { get; set; } = string.Empty;
    
    public int PointLimit { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedDate { get; set; }
}