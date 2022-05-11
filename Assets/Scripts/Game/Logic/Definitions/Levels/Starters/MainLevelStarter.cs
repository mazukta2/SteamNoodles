using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class MainLevelStarter : LevelStarter
    {
        public override void Start()
        {
            IScreenManagerView.Default.Presenter.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }
    }
}
