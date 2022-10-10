using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public abstract class LevelVariation
    {
        public string SceneName { get; set; }

        public abstract ILevel CreateModel(LevelDefinition definition, IModels models);
        public abstract void Validate();
    }
}
