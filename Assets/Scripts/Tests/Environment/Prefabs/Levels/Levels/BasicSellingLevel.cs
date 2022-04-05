using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;

namespace Game.Assets.Scripts.Tests.Mocks.Levels
{
    public class BasicSellingLevel : LevelPrefabMock
    {
        public override void FillLevel(LevelInTests level)
        {
            var screenSpawnPoint = new MockContainerView(level);
            new ScreenManagerView(level, screenSpawnPoint);

            var ghostContainer = new MockContainerView(level);
            var ghostPrototype = new MockPrototypeView(level, new GhostViewPrefab());
            new GhostManagerView(level, ghostContainer, ghostPrototype);

            var cellContainer = new MockContainerView(level);
            var cellPrototype = new MockPrototypeView(level, new CellViewPrefab());
            var constrcutionContainer = new MockContainerView(level);
            var constructionPrototype = new MockPrototypeView(level, new ConstructionViewPrefab());
            var placementManager = new PlacementManagerView(level, cellContainer, cellPrototype, constrcutionContainer, constructionPrototype);

            new PlacementFieldView(level, placementManager, 0);

            new UnitsManagerView(level, new MockContainerView(level), new MockPrototypeView(level, new UnitViewPrefab()));
        }

        private class GhostViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(ILevel level, MockContainerView container)
            {
                var contrainer = new MockContainerView(level);
                return new GhostView(level, contrainer, new LevelPosition(), new Rotator());
            }
        }

        private class CellViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(ILevel level, MockContainerView container)
            {
                return new CellView(level, new Enabler<CellPlacementStatus>(), new LevelPosition());
            }
        }

        private class ConstructionViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(ILevel level, MockContainerView container)
            {
                var c = new MockContainerView(level);
                return new ConstructionView(level, c, new LevelPosition(), new Rotator());
            }
        }

        private class UnitViewPrefab : MockViewPrefab
        {
            public override IView CreateView<T>(ILevel level, MockContainerView container) 
            {
                return new UnitView(level, new LevelPosition(), new Rotator(), new UnitAnimation());
            }
        }
    }
}
