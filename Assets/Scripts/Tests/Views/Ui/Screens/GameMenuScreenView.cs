using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class GameMenuScreenView : ScreenView<GameMenuScreenPresenter>, IGameMenuScreenView
    {
        public IButton Close { get; set; } = new ButtonMock();

        public IButton ExitGame { get; set; } = new ButtonMock();

        public GameMenuScreenView(IViews level) : base(level)
        {
        }
    }
}
