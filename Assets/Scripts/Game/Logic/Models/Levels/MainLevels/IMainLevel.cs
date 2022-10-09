using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Units;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Variations
{
    public interface IMainLevel : ILevel
    {
        new static IMainLevel Default { get; set; }

        event Action OnStart;
        FlowManager TurnManager { get; }
        PlayerHand Hand { get;}
        PlacementField Constructions { get; }
        LevelUnits Units { get; }
        CustomerQueue Queue { get; }
        Resources Resources { get; }
    }
}
