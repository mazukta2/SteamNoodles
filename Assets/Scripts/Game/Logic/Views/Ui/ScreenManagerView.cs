using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerView : View
    {
        public ContainerView Screen { get; }

        private ScreenManagerPresenter _presenter;

        public ScreenManagerPresenter GetPresenter() => _presenter;

        public ScreenManagerView(ILevel level, ContainerView screen) : base(level)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            Screen = screen;

            var assets = CoreAccessPoint.Core.Engine.Assets.Screens;
            _presenter = new ScreenManagerPresenter(this, assets);

            level.Services.Add(new ScreenManagerService(_presenter));
        }
    }
}
