using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Levels
{
    public interface ILevelSpecifics
    {
        Level CreateEntity(LevelDefinition definition);
        void StartServices(Level level);
        void StartLevel(Level level);
    }
}
