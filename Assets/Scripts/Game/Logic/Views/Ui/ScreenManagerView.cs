using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerView : View<ScreenManagerViewPresenter>
    {
        public ViewContainer Screen;

        private ScreenManagerViewPresenter _viewPresenter;
        public override ScreenManagerViewPresenter GetViewPresenter() => _viewPresenter;

        protected override void CreatedInner()
        {
            _viewPresenter = new ScreenManagerViewPresenter(Level, Screen.ViewPresenter);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

    public class ScreenManagerViewPresenter : ViewPresenter
    {
        public ContainerViewPresenter Screen { get; }

        private ScreenManagerPresenter _presenter;

        public ScreenManagerPresenter GetPresenter() => _presenter;

        public ScreenManagerViewPresenter(ILevel level, ContainerViewPresenter screen) : base(level)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            Screen = screen;

            var assets = CoreAccessPoint.Core.Engine.Assets.Screens;
            _presenter = new ScreenManagerPresenter(this, assets);
        }
    }
}
