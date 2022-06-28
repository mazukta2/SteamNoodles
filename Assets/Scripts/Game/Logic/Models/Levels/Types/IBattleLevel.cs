using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Types
{
    public interface IBattleLevel : ILevel
    {
        new static IBattleLevel Default { get; set; }

        FlowManager TurnManager { get; }
        PlayerHand Hand { get;}
        PlacementField Constructions { get; }
        LevelUnits Units { get; }
        CustomerQueue Queue { get; }
        Resources Resources { get; }
    }
}
