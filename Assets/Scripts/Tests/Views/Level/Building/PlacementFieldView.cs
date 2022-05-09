using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PlacementFieldView : PresenterView<PlacementFieldPresenter>, IPlacementFieldView
    {
        public IViewContainer ConstrcutionContainer { get; private set; }
        public IViewPrefab ConstrcutionPrototype { get; private set; }
        public IViewContainer CellsContainer { get; private set; }
        public IViewPrefab Cell { get; private set; }

        public PlacementFieldView(LevelView level, ViewPrefabMock construction, ViewPrefabMock cell) : base(level)
        {
            ConstrcutionContainer = new ContainerViewMock(level);
            Cell = new PrototypeViewMock(level, cell);
            CellsContainer = new ContainerViewMock(level);
            ConstrcutionPrototype = new PrototypeViewMock(level, construction);
        }
    }
}