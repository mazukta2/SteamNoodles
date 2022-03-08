using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public interface ILevelDefinition : IHandDefinition
    {
        string SceneName { get; }
        
        IReadOnlyCollection<IConstructionDefinition> StartingHand { get; }
    }
}
