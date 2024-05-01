using Battleforged.ArmyService.Domain.Entities;

namespace Battleforged.ArmyService.Infrastructure.Database.Data; 

public static class BattleSizes {

    public static IReadOnlyList<BattleSize> Data = new List<BattleSize>() {
        new BattleSize {
            Id  = Guid.Parse("273bf3bf714a478fbc44d07bd9f7c480"),
            Description = "Incursion",
            PointLimit = 1000,
        },
        new BattleSize {
            Id  = Guid.Parse("19147fa23004411d9992223c19eb8c9f"),
            Description = "Strike Force",
            PointLimit = 2000,
        },
        new BattleSize {
            Id  = Guid.Parse("dcf594c8a7b046af9714ebce6b58605b"),
            Description = "Onslaught",
            PointLimit = 3000,
        }
    };
}