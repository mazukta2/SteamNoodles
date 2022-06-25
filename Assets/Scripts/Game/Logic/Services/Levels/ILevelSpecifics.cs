using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;

namespace Game.Assets.Scripts.Game.Logic.Services.Levels
{
    public interface ILevelSpecifics
    {
        Level CreateEntity(LevelDefinition definition, ServiceManager services);
        void StartServices(Level level);
        void StartLevel(Level level);
    }
}
