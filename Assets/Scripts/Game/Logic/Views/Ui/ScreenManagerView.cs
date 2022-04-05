using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerView : PresenterView<ScreenManagerPresenter>
    {
        public IViewContainer Screen { get; }

        public ScreenManagerView(ILevel level, IViewContainer screen) : base(level)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            Screen = screen;

            var assets = CoreAccessPoint.Core.Engine.Assets.Screens;
            var presenter = new ScreenManagerPresenter(this, assets);
            level.Services.Add(new ScreenManagerService(presenter));
        }
    }
}
