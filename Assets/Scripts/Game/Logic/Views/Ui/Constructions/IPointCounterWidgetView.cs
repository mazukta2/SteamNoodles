using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;

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

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this; 
            new PointCounterWidgetPresenter(IBattleLevel.Default.Resources, 
                IGhostManagerView.Default.Presenter, 
                IBattleLevel.Default.Constructions, IGameTime.Default, this);
        }
    }
}
