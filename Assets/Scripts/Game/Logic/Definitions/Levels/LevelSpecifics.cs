using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public abstract class LevelSpecifics : ILevelSpecifics
    {
        public abstract Level CreateEntity(LevelDefinition definition, ServiceManager services);
        public abstract void StartServices(Level level);
        public abstract void StartLevel(Level level);
    }
}
