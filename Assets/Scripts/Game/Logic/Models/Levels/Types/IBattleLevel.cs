using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Types
{
    public interface IBattleLevel : ILevel
    {
        new static IBattleLevel Default { get; set; }

        FlowManager Flow { get; }
        BuildingService Building { get; }
        SchemesService Schemes { get; }
        HandService Hand { get;}
        UnitsService Units { get; }
        FieldService Field { get; }
        UnitsCustomerQueueService Queue { get; }
        Resources Resources { get; }
    }
}
