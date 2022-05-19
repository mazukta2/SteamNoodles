using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public abstract class LevelStarter
    {
        public abstract ILevel CreateModel(LevelDefinition definition);
        public abstract void Start();
    }
}
