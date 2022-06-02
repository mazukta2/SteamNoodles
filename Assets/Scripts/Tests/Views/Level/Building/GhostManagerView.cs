using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using System;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class GhostManagerView : ViewWithPresenter<GhostManagerPresenter>, IGhostManagerView
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab GhostPrototype { get; private set; }
        public GhostManagerView(IViewsCollection level) : base(level)
        {
            Container = new ContainerViewMock(level);
            GhostPrototype = new GhostViewPrefab();
        }

        private class GhostViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViewsCollection collection)
            {
                new GhostView(collection);
            }
        }
    }
}