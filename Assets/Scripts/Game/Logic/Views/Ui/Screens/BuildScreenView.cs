using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView
    {
        private BuildScreenPresenter _presenter;
        public BuildScreenView(ILevel level) : base(level)
        {
        }

        public override void SetManager(ScreenManagerPresenter manager)
        {
            base.SetManager(manager);
            _presenter = new BuildScreenPresenter(this, manager);
        }
    }
}
