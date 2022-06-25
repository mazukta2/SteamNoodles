using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions
{
    public interface IPointCounterWidgetView : IViewWithGenericPresenter<PointCounterWidgetPresenter>, IViewWithDefaultPresenter
    {
        IText Points { get; }
        IPointsProgressBar PointsProgress { get; }

        IPosition PointsAttractionPoint { get; }
        IPosition PointsAttractionControlPoint { get; }
        IAnimator Animator { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new PointCounterWidgetPresenter(this);
        }
    }
}
