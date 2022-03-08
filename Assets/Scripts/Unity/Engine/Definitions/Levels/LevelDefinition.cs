using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System.Collections.Generic;

namespace GameUnity.Assets.Scripts.Unity.Engine.Definitions.Levels
{
    public class LevelDefinition : ILevelDefinition
    {
        public string SceneName { get; set; }

        public IReadOnlyCollection<IConstructionDefinition> StartingHand => throw new System.NotImplementedException();

        public int HandSize => throw new System.NotImplementedException();
    }
}
