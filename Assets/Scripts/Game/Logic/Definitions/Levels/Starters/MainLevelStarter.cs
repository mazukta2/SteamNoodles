using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class MainLevelStarter : LevelStarter
    {
        //public override IGameLevel CreateModel(LevelDefinition definition)
        //{
        //    return new StageLevelService(definition, IGameRandom.Default, IGameTime.Default, IGameDefinitions.Default);
        //}

        public override void Start()
        {
            ScreenManagerPresenter.Default.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }
    }
}
