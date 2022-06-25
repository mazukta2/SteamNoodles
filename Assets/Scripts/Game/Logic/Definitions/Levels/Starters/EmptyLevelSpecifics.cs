using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class EmptyLevelSpecifics : LevelSpecifics
    {
        public override Level CreateEntity(LevelDefinition definition, ServiceManager services)
        {
            return new Level(definition);
        }

        public override void StartLevel(Level level)
        {
        }

        public override void StartServices(Level level)
        {
        }
    }
}
