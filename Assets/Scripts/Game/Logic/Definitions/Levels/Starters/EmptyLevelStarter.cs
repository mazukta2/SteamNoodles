using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class EmptyLevelStarter : LevelStarter
    {
        //public override IGameLevel CreateModel(LevelDefinition definition)
        //{
        //    return new StageLevelService(definition, IGameRandom.Default, IGameTime.Default, IGameDefinitions.Default);
        //}

        public override void Start()
        {
        }
    }
}
