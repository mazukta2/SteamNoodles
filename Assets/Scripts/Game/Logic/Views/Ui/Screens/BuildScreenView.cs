using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView<BuildScreenViewPresenter>
    {
        private BuildScreenViewPresenter _viewPresenter;
        public override BuildScreenViewPresenter GetViewPresenter() => _viewPresenter;
        protected override void CreatedInner()
        {
            _viewPresenter = new BuildScreenViewPresenter(Level);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

}
