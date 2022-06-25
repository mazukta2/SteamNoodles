using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level
{
    public class ConstructionView : ViewWithPresenter<ConstructionPresenter>, IConstructionView
    {
        public IPosition Position { get; set; } = new PositionMock();
        public IRotator Rotator { get; }
        public IViewContainer Container { get; set; }

        public IViewContainer EffectsContainer { get; set; }
        public IViewPrefab ExplosionPrototype { get; set; }

        public ConstructionView(IViewsCollection level) : base(level)
        {
            Container = new ContainerViewMock(level);
            Rotator = new Rotator();
            EffectsContainer = new ContainerViewMock(level);
            ExplosionPrototype = new DefaultViewPrefab(CreateExplosion);
        }

        private void CreateExplosion(IViewsCollection obj)
        {
        }

        public void Init(ISingleQuery<Construction> construction)
        {
             construction.Dispose();
        }
    }
}
