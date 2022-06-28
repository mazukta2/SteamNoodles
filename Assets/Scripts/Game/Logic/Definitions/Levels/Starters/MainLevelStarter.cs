using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class MainLevelStarter : LevelStarter
    {
        public override ILevel CreateModel(LevelDefinition definition)
        {
            return new GameLevel(definition, IGameRandom.Default, IGameTime.Default, IGameDefinitions.Default);
        }

        public override void Start()
        {
            IScreenManagerView.Default.Presenter.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }
    }
}
