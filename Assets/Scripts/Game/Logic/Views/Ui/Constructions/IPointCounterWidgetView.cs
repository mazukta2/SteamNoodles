using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
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

        static IPointCounterWidgetView Default { get; set; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this; 
            new PointCounterWidgetPresenter(IModels.Default.Find<BuildingPointsManager>(), 
                IGhostManagerView.Default.Presenter, IGameTime.Default, this, IPointPieceSpawnerView.Default,
                IDefinitions.Default.Get<ConstructionsSettingsDefinition>());
        }
    }
}
