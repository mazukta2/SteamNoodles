using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class StageLevelSpecifics : LevelSpecifics
    {
        public override Level CreateEntity(LevelDefinition definition)
        {
            return new StageLevel(definition);
        }

        public override void StartLevel(Level level)
        {
            //ScreenManagerPresenter.Default.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        public override void StartServices(Level level)
        {
            //new StageLevelService(level, IGameRandom.Default, IGameTime.Default, IGameDefinitions.Default);
        }
    }
}
