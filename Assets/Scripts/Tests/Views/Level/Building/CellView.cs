using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class CellView : ViewWithPresenter<PlacementCellPresenter>, ICellView
    {
        public ILevelPosition LocalPosition { get; private set; }
        public ISwitcher<CellPlacementStatus> State { get; private set; }

        public IAnimator Animator { get; } = new AnimatorMock();

        public CellView(IViewsCollection level, ISwitcher<CellPlacementStatus> state, ILevelPosition position) : base(level)
        {
            State = state;
            LocalPosition = position;
        }
    }
}