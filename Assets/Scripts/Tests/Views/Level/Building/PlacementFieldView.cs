using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PlacementFieldView : ViewWithPresenter<PlacementFieldPresenter>, IPlacementFieldView
    {
        public IViewContainer ConstrcutionContainer { get; private set; }
        public IViewPrefab ConstrcutionPrototype { get; private set; }
        public IViewContainer CellsContainer { get; private set; }
        public IViewPrefab Cell { get; private set; }

        public PlacementFieldView(IViewsCollection level, ViewCollectionPrefabMock construction, ViewCollectionPrefabMock cell) : base(level)
        {
            ConstrcutionContainer = new ContainerViewMock(level);
            Cell = cell;
            CellsContainer = new ContainerViewMock(level);
            ConstrcutionPrototype = construction;
        }
    }
}