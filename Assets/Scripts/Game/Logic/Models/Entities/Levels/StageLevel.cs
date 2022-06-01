using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record StageLevel : Entity
    {
        public IReadOnlyCollection<ConstructionScheme> StartingSchemes { get; } = new List<ConstructionScheme>();

        private DefId _defintionId; // this information only for saves. don't expose this field

        public StageLevel(Uid id, LevelDefinition definition) : base(id)
        {
            _defintionId = definition.DefId;
        }

        public StageLevel(LevelDefinition definition)
        {
            _defintionId = definition.DefId;
        }

        public StageLevel(IEnumerable<ConstructionScheme> startingSchemes)
        {
            StartingSchemes = startingSchemes.AsReadOnly();
            _defintionId = DefId.None;
        }

        public StageLevel()
        {
            _defintionId = DefId.None;
        }

    }
}
