namespace Battleforged.ArmyService.Endpoints.Armies.FetchArmiesPaged;

public sealed class FetchArmiesPagedResponse {
    
    public Guid Id { get; set; }
    
    public Guid? ParentArmyId { get; set; }

    public string Name { get; set; } = string.Empty;
}