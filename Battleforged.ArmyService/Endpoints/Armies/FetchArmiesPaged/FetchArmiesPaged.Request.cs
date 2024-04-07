namespace Battleforged.ArmyService.Endpoints.Armies.FetchArmiesPaged;

public sealed class FetchArmiesPagedRequest {
    
    public Guid? Cursor { get; set; }

    public int Limit { get; set; } = 50;
}