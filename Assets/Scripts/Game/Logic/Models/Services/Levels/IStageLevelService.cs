using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Levels
{
    public interface IStageLevelService : IGameLevel
    {
        new static IStageLevelService Default { get; set; }

        StageTurnService Flow { get; }
        BuildingService Building { get; }
        SchemesService Schemes { get; }
        HandService Hand { get; }
        UnitsService Units { get; }
        FieldService Field { get; }
        UnitsCustomerQueueService Queue { get; }
        CoinsService Coins { get; }
        BuildingPointsService Points { get; }
    }
}
