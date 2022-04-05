﻿using Game.Assets.Scripts.Game.Environment.Engine;
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
            var screenSpawnPoint = new TestContainerView(level);
            new ScreenManagerView(level, screenSpawnPoint);

            var ghostContainer = new TestContainerView(level);
            var ghostPrototype = new TestPrototypeView(level, new GhostViewPrefab());
            new GhostManagerView(level, ghostContainer, ghostPrototype);

            var cellContainer = new TestContainerView(level);
            var cellPrototype = new TestPrototypeView(level, new CellViewPrefab());
            var constrcutionContainer = new TestContainerView(level);
            var constructionPrototype = new TestPrototypeView(level, new ConstructionViewPrefab());
            var placementManager = new PlacementManagerView(level, cellContainer, cellPrototype, constrcutionContainer, constructionPrototype);

            new PlacementFieldView(level, placementManager, 0);

            new UnitsManagerView(level, new TestContainerView(level), new TestPrototypeView(level, new UnitViewPrefab()));
        }

        private class GhostViewPrefab : TestViewPrefab
        {
            public override View CreateView<T>(ILevel level, TestContainerView container)
            {
                var contrainer = new TestContainerView(level);
                return new GhostView(level, contrainer, new LevelPosition(), new Rotator());
            }
        }

        private class CellViewPrefab : TestViewPrefab
        {
            public override View CreateView<T>(ILevel level, TestContainerView container)
            {
                return new CellView(level, new Enabler<CellPlacementStatus>(), new LevelPosition());
            }
        }

        private class ConstructionViewPrefab : TestViewPrefab
        {
            public override View CreateView<T>(ILevel level, TestContainerView container)
            {
                var c = new TestContainerView(level);
                return new ConstructionView(level, c, new LevelPosition(), new Rotator());
            }
        }

        private class UnitViewPrefab : TestViewPrefab
        {
            public override View CreateView<T>(ILevel level, TestContainerView container) 
            {
                return new UnitView(level, new LevelPosition(), new Rotator(), new UnitAnimation());
            }
        }
    }
}
