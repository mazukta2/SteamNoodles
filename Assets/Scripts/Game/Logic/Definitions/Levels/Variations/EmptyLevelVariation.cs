using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations
{
    public class EmptyLevelVariation : LevelVariation
    {
        public override (ILevel, IPresenter) CreateModel(LevelDefinition definition, IModels models)
        {
            return (null, null);
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(SceneName))
                throw new Exception();
        }
    }
}
