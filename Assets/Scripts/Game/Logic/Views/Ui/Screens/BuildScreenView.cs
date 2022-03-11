using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView
    {
        public BuildScreenView(ILevel level) : base(level)
        {
        }

        public override void SetManager(ScreenManagerPresenter manager)
        {
            base.SetManager(manager);
            Presenter = new BuildScreenPresenter(this, manager);
        }
    }
}
