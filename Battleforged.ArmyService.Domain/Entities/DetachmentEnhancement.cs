namespace Battleforged.ArmyService.Domain.Entities; 

public sealed class DetachmentEnhancement {
    
    public Guid Id { get; set; }
    
    public Guid DetachmentId { get; set; }

    public string EnhancementName { get; set; } = string.Empty;

    public string? EnhancementText { get; set; }
    
    public int PointCost { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedDate { get; set; }
}