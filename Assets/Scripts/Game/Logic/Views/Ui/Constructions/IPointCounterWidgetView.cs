using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions
{
    public interface IPointCounterWidgetView : IViewWithGenericPresenter<PointCounterWidgetPresenter>, IViewWithDefaultPresenter
    {
        IText Points { get; }
        IPointsProgressBar PointsProgress { get; }

        IPosition PointsAttractionPoint { get; }
        IPosition PointsAttractionControlPoint { get; }
        IAnimator Animator { get; }

        static IPointCounterWidgetView Default { get; set; }

        void IViewWithDefaultPresenter.Init()
        {
            Default = this; 
            new PointCounterWidgetPresenter(ICurrentLevel.Default.Resources, 
                IGhostManagerPresenter.Default, 
                ICurrentLevel.Default.Constructions, ICurrentLevel.Default.Time, this);
        }
    }
}
