using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Views;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui
{
    public class ScreenManagerView : PresenterView<ScreenManagerPresenter>, IScreenManagerView
    {
        public IViewContainer Screen { get; }

        public ScreenManagerView(LevelView level, IViewContainer screen) : base(level)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            Screen = screen;
        }
    }
}
