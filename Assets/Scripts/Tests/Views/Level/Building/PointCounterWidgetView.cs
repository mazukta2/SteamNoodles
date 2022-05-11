using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PointCounterWidgetView : ViewWithPresenter<PointCounterWidgetPresenter>, IPointCounterWidgetView
    {
        public IPosition PointsAttractionPoint { get; set; } = new PositionMock();
        public IPosition PointsAttractionControlPoint { get; set; } = new PositionMock();
        public IText Points { get; set; } = new TextMock();
        public IPointsProgressBar PointsProgress { get; set; } = new PointsProgressBarMock();
        public IAnimator Animator { get; set; } = new AnimatorMock();

        public PointCounterWidgetView(ILevelView level) : base(level)
        {
        }

    }
}