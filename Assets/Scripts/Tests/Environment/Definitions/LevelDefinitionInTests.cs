using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using System;
using System.Collections.Generic;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelDefinitionInTests : ILevelDefinition
    {
        public string SceneName => throw new NotImplementedException();

        public List<IConstructionDefinition> StartingHand { get; set; } = new List<IConstructionDefinition>();
        IReadOnlyCollection<IConstructionDefinition> ILevelDefinition.StartingHand => StartingHand.AsReadOnly();

        public int HandSize { get; set; } = 5;
        
        public LevelMockCreator Creator { get; private set; }
        public LevelDefinitionInTests(LevelMockCreator creator)
        {
            Creator = creator;
        }

    }
}
