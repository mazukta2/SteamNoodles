using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class CellView : ViewWithPresenter<PlacementCellPresenter>, ICellView
    {
        public IPosition LocalPosition { get; private set; } = new PositionMock();
        public ISwitcher<CellPlacementStatus> State { get; private set; }

        public IAnimator Animator { get; } = new AnimatorMock();

        public CellView(IViewsCollection level, ISwitcher<CellPlacementStatus> state) : base(level)
        {
            State = state;
        }
    }
}