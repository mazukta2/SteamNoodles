using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class GhostManagerView : PresenterView<GhostManagerPresenter>, IGhostManagerView
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab GhostPrototype { get; private set; }
        public GhostManagerView(LevelView level, IViewContainer container, IViewPrefab ghostPrototype) : base(level)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            GhostPrototype = ghostPrototype ?? throw new ArgumentNullException(nameof(ghostPrototype));
        }
    }
}