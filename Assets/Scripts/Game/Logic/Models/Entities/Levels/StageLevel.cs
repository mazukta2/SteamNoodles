using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record StageLevel : Level
    {
        public IReadOnlyCollection<ConstructionScheme> StartingSchemes { get; } = new List<ConstructionScheme>();

        public StageLevel(LevelDefinition definition) : base(definition)
        {
        }

        public StageLevel(IEnumerable<ConstructionScheme> startingSchemes)
        {
            StartingSchemes = startingSchemes.AsReadOnly();
        }

        public StageLevel()
        {
        }
    }
}
