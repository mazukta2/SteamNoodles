using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PlacementFieldView : ViewWithPresenter<PlacementFieldPresenter>, IPlacementFieldView
    {
        public IViewContainer ConstrcutionContainer { get; private set; }
        public IViewPrefab ConstrcutionPrototype { get; private set; }
        public IViewContainer CellsContainer { get; private set; }
        public IViewPrefab Cell { get; private set; }

        public PlacementFieldView(IViewsCollection level) : base(level)
        {
            ConstrcutionContainer = new ContainerViewMock(level);
            Cell = new CellViewPrefab();
            CellsContainer = new ContainerViewMock(level);
            ConstrcutionPrototype = new ConstructionViewPrefab();
        }

        private class CellViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViewsCollection collection)
            {
                new CellView(collection, new Switcher<CellPlacementStatus>());
            }
        }

        private class ConstructionViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViewsCollection collection)
            {
                var c = new ContainerViewMock(collection);
                new ConstructionView(collection, c, new Rotator());
            }
        }

    }
}