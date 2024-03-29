﻿using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels
{
    public class BasicMainLevel : ViewCollectionPrefabMock
    {
        public override void Fill(IViews collection)
        {
            var screenSpawnPoint = new ContainerViewMock(collection);
            new ScreenManagerView(collection, screenSpawnPoint);

            var ghostContainer = new ContainerViewMock(collection);
            var ghostPrototype = new GhostViewPrefab();
            new GhostManagerView(collection, ghostContainer, ghostPrototype);

            new PlacementFieldView(collection, new ConstructionViewPrefab(), new CellViewPrefab());

            new UnitsManagerView(collection);

            new PieceSpawnerView(collection);

            new PointCounterWidgetView(collection);

            new HandView(collection);

            new BuildingTooltipMock(collection);

            new WaveWidgetView(collection);
        }

        private class GhostViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                var contrainer = new ContainerViewMock(collection);
                new GhostView(collection, contrainer, new Rotator());
            }
        }

        private class CellViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                new CellView(collection, new Switcher<CellPlacementStatus>());
            }
        }

        private class ConstructionViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                var c = new ContainerViewMock(collection);
                new ConstructionView(collection, c, new Rotator());
            }
        }

        private class UnitViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                new UnitView(collection, new Rotator(), new AnimatorMock(), new UnitDresser());
            }
        }
    }
}
