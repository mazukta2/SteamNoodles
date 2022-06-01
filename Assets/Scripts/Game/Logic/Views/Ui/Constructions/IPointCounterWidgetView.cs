using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
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

        static IPointCounterWidgetView Default { get; set; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this; 
            new PointCounterWidgetPresenter(IStageLevelService.Default.Points, 
                IGhostManagerView.Default.Presenter, IGameTime.Default, this, IPointPieceSpawnerView.Default,
                IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>());
        }
    }
}
