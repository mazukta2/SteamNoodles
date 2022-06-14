using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface ICellView : IViewWithPresenter
    {
        IPosition LocalPosition { get; }
        ISwitcher<CellPlacementStatus> State { get; }
        IAnimator Animator { get; }
    }
}