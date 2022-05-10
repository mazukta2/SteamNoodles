using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels
{
    public class BasicSellingLevel : LevelPrefabMock
    {
        public override void FillLevel(ILevelView level)
        {
            var screenSpawnPoint = new ContainerViewMock(level);
            new ScreenManagerView(level, screenSpawnPoint);

            var ghostContainer = new ContainerViewMock(level);
            var ghostPrototype = new PrototypeViewMock(level, new GhostViewPrefab());
            new GhostManagerView(level, ghostContainer, ghostPrototype);

            new PlacementFieldView(level, new ConstructionViewPrefab(), new CellViewPrefab());

            new UnitsManagerView(level, new ContainerViewMock(level), new PrototypeViewMock(level, new UnitViewPrefab()));

            new PieceSpawnerView(level);

            new PointAttractionPositionView(level);
        }

        private class GhostViewPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                var contrainer = new ContainerViewMock(level);
                return new GhostView(level, contrainer, new LevelPosition(), new Rotator());
            }
        }

        private class CellViewPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                return new CellView(level, new Switcher<CellPlacementStatus>(), new LevelPosition());
            }
        }

        private class ConstructionViewPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                var c = new ContainerViewMock(level);
                return new ConstructionView(level, c, new LevelPosition(), new Rotator());
            }
        }

        private class UnitViewPrefab : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                return new UnitView(level, new LevelPosition(), new Rotator(), new AnimatorMock(), new UnitDresser());
            }
        }
    }
}
