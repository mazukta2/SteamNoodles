using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public abstract class LevelVariation
    {
        public string SceneName { get; set; }

        public abstract (ILevel, IPresenter) CreateModel(LevelDefinition definition, IModels models);
        public abstract void Validate();
    }
}
