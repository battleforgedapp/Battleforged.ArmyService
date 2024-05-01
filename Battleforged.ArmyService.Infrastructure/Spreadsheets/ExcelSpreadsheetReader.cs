using System.Data;
using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Enums;
using Battleforged.ArmyService.Domain.Repositories;
using ExcelDataReader;

namespace Battleforged.ArmyService.Infrastructure.Spreadsheets; 

/// <inheritdoc cref="ISpreadsheetImporter" />
public sealed class ExcelSpreadsheetReader(
    IArmyRepository armyRepository,
    IDetachmentRepository detachmentRepository,
    IDetachmentEnhancementRepository detachmentEnhancementRepository,
    IUnitRepository unitRepository,
    IUnitGroupingRepository unitGroupingRepository,
    IUnitOfWork unitOfWork     
) : ISpreadsheetImporter {

    private const string ArmyTableName = "armies";
    private const string UnitTableName = "army_units";
    private const string DetachmentTableName = "army_detachments";
    private const string DetachmentEnhancementTableName = "army_detachment_enhancements";
    
    public async Task<bool> ImportServiceDataFromSpreadsheetAsync(Stream fileStream, CancellationToken ct = default) {
        await unitOfWork.BeginTransactionAsync(ct);
        try {
            // use the file-stream for the request/command to open the excel spreadsheet
            using var reader = ExcelReaderFactory.CreateReader(fileStream);
            var result = reader.AsDataSet();
        
            // build the data we want to import
            var armies = FetchArmyData(result.Tables[ArmyTableName]);
            var units = FetchUnitData(result.Tables[UnitTableName]);
            var detachments = FetchDetachmentData(result.Tables[DetachmentTableName]);
            var enhancements = FetchDetachmentEnhancementData(result.Tables[DetachmentEnhancementTableName]);
            
            // import all the data within the same transaction
            // NOTE: this will update existing records so that we can run this more than once to get refreshed data!
            await armyRepository.AddRangeAsync(armies, ct);
            await detachmentRepository.AddRangeAsync(detachments, ct);
            await detachmentEnhancementRepository.AddRangeAsync(enhancements, ct);
            await unitRepository.AddRangeAsync(units.Select(x => x.Unit).ToList(), ct);
            await unitGroupingRepository.AddRangeAsync(units.SelectMany(x => x.Groupings).ToList(), ct);
            
            await unitOfWork.CommitAsync(ct);
            return true;
        }
        catch {
            await unitOfWork.RollbackAsync(ct);
            // TODO: return meaningful error?
            throw;
        }
    }
    
    private static IReadOnlyList<Army> FetchArmyData(DataTable? table) {
        // throw an exception when the table data cannot be read?
        if (table is null) {
            throw new Exception($"Could not find '{ArmyTableName}' worksheet data.");
        }
        
        var results = new List<Army>();

        // NOTE: use for-loop so we can skip over the header row (index: 0)
        for (var i = 1; i < table.Rows.Count; i++) {
            // read all the data from the current table row
            var dataRow = table.Rows[i];
            var armyId = dataRow.Field<string>(table.Columns[0])!;
            var parentId = dataRow.Field<string>(table.Columns[1]);
            var typeText = dataRow.Field<string>(table.Columns[2]);
            var armyName = dataRow.Field<string>(table.Columns[3])!;
            var isActive = dataRow.Field<double>(table.Columns[4]);

            // this will convert my text to the enum value
            var armyType = typeText switch {
                "XENOS" => ArmyTypes.Xenos,
                "CHAOS" => ArmyTypes.Chaos,
                "SPACE MARINES" => ArmyTypes.SpaceMarines, 
                _ => ArmyTypes.Imperium
            };
            
            // add the object to the result list
            // NOTE: we can use a flag to "delete" via the data
            // this helps in a re-upload situation
            results.Add(new Army {
                Id = Guid.Parse(armyId),
                ParentArmyId = string.IsNullOrWhiteSpace(parentId) ? null : Guid.Parse(parentId),
                Name = armyName,
                Type = armyType,
                DeletedDate = isActive < 1 ? DateTime.UtcNow : null
            });
        }
        
        return results;
    }

    private static IReadOnlyList<Detachment> FetchDetachmentData(DataTable? table) {
        // throw an exception when the table data cannot be read?
        if (table is null) {
            throw new Exception($"Could not find '{DetachmentTableName}' worksheet data.");
        }
        
        var results = new List<Detachment>();

        // NOTE: use for-loop so we can skip over the header row (index: 0)
        for (var i = 1; i < table.Rows.Count; i++) {
            // read all the data from the current table row
            var dataRow = table.Rows[i];
            var armyId = dataRow.Field<string>(table.Columns[0])!;
            var detachmentId = dataRow.Field<string>(table.Columns[1])!;
            var detachmentName = dataRow.Field<string>(table.Columns[2])!;
            var ruleName = dataRow.Field<string?>(table.Columns[3]);
            var ruleText = dataRow.Field<string?>(table.Columns[4]);
            var isActive = dataRow.Field<double>(table.Columns[5]);
            
            // add the object to the result list
            // NOTE: we can use a flag to "delete" via the data
            // this helps in a re-upload situation
            results.Add(new Detachment {
                Id = Guid.Parse(detachmentId),
                ArmyId = Guid.Parse(armyId),
                DetachmentName = detachmentName,
                RuleName = ruleName,
                RuleText = ruleText,
                DeletedDate = isActive < 1 ? DateTime.UtcNow : null
            });
        }
        
        return results;
    }

    private static IReadOnlyList<DetachmentEnhancement> FetchDetachmentEnhancementData(DataTable? table) {
        // throw an exception when the table data cannot be read?
        if (table is null) {
            throw new Exception($"Could not find '{DetachmentEnhancementTableName}' worksheet data.");
        }
        
        var results = new List<DetachmentEnhancement>();

        // NOTE: use for-loop so we can skip over the header row (index: 0)
        for (var i = 1; i < table.Rows.Count; i++) {
            // read all the data from the current table row
            var dataRow = table.Rows[i];
            var detachmentId = dataRow.Field<string>(table.Columns[0])!;
            var enhancementId = dataRow.Field<string>(table.Columns[1])!;
            var enhancementName = dataRow.Field<string>(table.Columns[2])!;
            var enhancementText = dataRow.Field<string?>(table.Columns[3]);
            var pointCost = dataRow.Field<double>(table.Columns[4]);
            var isActive = dataRow.Field<double>(table.Columns[5]);
            
            // add the object to the result list
            // NOTE: we can use a flag to "delete" via the data
            // this helps in a re-upload situation
            results.Add(new DetachmentEnhancement {
                Id = Guid.Parse(enhancementId),
                DetachmentId = Guid.Parse(detachmentId),
                EnhancementName = enhancementName,
                EnhancementText = enhancementText,
                PointCost = (int)pointCost,
                DeletedDate = isActive < 1 ? DateTime.UtcNow : null
            });
        }
        
        return results;
    }
    
    private static IReadOnlyList<UnitDetails> FetchUnitData(DataTable? table) {
        // throw an exception when the table data cannot be read?
        if (table is null) {
            throw new Exception($"Could not find '{UnitTableName}' worksheet data.");
        }
        
        // NOTE: use for-loop so we can skip over the header row (index: 0)
        var data = new List<UnitRecord>();
        for (var i = 1; i < table.Rows.Count; i++) {
            // read all the data from the current table row
            var dataRow = table.Rows[i];
            var armyId = dataRow.Field<string>(table.Columns[0])!;
            var unitId = dataRow.Field<string>(table.Columns[1])!;
            var groupId = dataRow.Field<string>(table.Columns[2])!;
            var unitName = dataRow.Field<string>(table.Columns[3])!;
            var unitSize = dataRow.Field<double>(table.Columns[4]);
            var unitCost = dataRow.Field<double>(table.Columns[5]);
            var isActive = dataRow.Field<double>(table.Columns[6]);
            
            data.Add(new UnitRecord(
                Guid.Parse(groupId),
                Guid.Parse(unitId),
                Guid.Parse(armyId),
                unitName,
                (int) unitSize,
                (int) unitCost,
                isActive > 0
            ));
        }
        
        // now we can group all this data to get the army domain models and the grouping domain models
        var results = data
            .GroupBy(x => x.UnitId)
            .Select(x => new UnitDetails(
                x.Key,
                new Unit {
                    Id = x.Key,
                    ArmyId = x.First().ArmyId,
                    UnitName = x.First().UnitName,
                    CreatedDate = DateTime.UtcNow,
                    DeletedDate = x.All(y => !y.IsActive) ? DateTime.UtcNow : null
                },
                x.Select(y => new UnitGrouping {
                    Id = y.GroupId,
                    UnitId = y.UnitId,
                    ModelCount = y.GroupSize,
                    PointCost = y.GroupCost,
                    CreatedDate = DateTime.UtcNow,
                    DeletedDate = y.IsActive ? null : DateTime.UtcNow
                }).ToList()
            ))
            .ToList();
        
        return results;
    }

    private record UnitDetails(Guid UnitId, Unit Unit, IReadOnlyList<UnitGrouping> Groupings);

    private record UnitRecord(Guid GroupId, Guid UnitId, Guid ArmyId, string UnitName, int GroupSize, int GroupCost, bool IsActive);
}