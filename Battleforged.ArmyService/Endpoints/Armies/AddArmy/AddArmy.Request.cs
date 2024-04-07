namespace Battleforged.ArmyService.Endpoints.Armies.AddArmy;

public sealed class AddArmyRequest {
    
    public Guid? ParentArmyId { get; set; }

    public string Name { get; set; } = string.Empty;
}