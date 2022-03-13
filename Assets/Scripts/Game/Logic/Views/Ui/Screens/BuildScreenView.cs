using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView
    {
        public ButtonView CancelButton { get; }

        public BuildScreenView(ILevel level, ButtonView cancelButton) : base(level)
        {
            CancelButton = cancelButton;
        }

        public override void SetManager(ScreenManagerPresenter manager)
        {
            base.SetManager(manager);
            Presenter = new BuildScreenPresenter(this, manager);
        }
    }
}
