namespace Battleforged.ArmyService.Domain.Entities; 

public sealed class UnitGrouping {
    
    public Guid Id { get; set; }
    
    public Guid UnitId { get; set; }
    
    public int ModelCount { get; set; }
    
    public int PointCost { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedDate { get; set; }
}