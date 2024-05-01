namespace Battleforged.ArmyService.Domain.Abstractions; 

public interface ISpreadsheetImporter {
    Task<bool> ImportServiceDataFromSpreadsheetAsync(Stream fileStream, CancellationToken ct = default);
}