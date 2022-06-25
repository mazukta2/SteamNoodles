using Game.Assets.Scripts.Game.Logic.Common.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Services.Levels;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public abstract class LevelSpecifics : ILevelSpecifics
    {
        public abstract Level CreateEntity(LevelDefinition definition, ServiceManager services);
        public abstract void StartServices(Level level);
        public abstract void StartLevel(Level level);
    }
}
