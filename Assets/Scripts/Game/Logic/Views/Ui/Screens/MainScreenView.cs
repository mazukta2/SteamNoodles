using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class MainScreenView : CommonScreenView
    {
        public HandView HandView { get; set; }
        public IText Points { get; set; }

        public MainScreenView(ILevel level, HandView handView, IText points) : base(level)
        {
            HandView = handView;
            Points = points;
        }

        public override void Init(ScreenManagerPresenter manager)
        {
            Presenter = new MainScreenPresenter(this, manager, Level.Model.Resources);
        }
    }
}
