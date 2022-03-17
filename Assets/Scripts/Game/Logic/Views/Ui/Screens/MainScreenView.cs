using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class MainScreenView : CommonScreenView
    {
        public HandView HandView;

        public MainScreenView(ILevel level, HandView handView) : base(level)
        {
            HandView = handView;
        }

        public override void Init(ScreenManagerPresenter manager)
        {
            Presenter = new MainScreenPresenter(this, manager);
        }
    }
}
