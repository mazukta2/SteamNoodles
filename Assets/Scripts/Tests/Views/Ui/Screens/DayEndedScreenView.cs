using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class DayEndedScreenView : ScreenView<DayEndedScreenPresenter>, IDayEndedScreenView
    {
        public IButton NextDayButton { get; set; } = new ButtonMock();

        public DayEndedScreenView(IViewsCollection level) : base(level)
        {
        }
    }
}
