using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class MainScreenView : ScreenView<MainScreenPresenter>, IMainScreenView
    {

        public MainScreenView(IViewsCollection level) : base(level)
        {
        }
    }
}
