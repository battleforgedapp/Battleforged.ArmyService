namespace Battleforged.ArmyService.Domain.Entities; 

public sealed class Detachment {
    
    public Guid Id { get; set; }
    
    public Guid ArmyId { get; set; }

    public string DetachmentName { get; set; } = string.Empty;

    public string? RuleName { get; set; }

    public string? RuleText { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? DeletedDate { get; set; }
}