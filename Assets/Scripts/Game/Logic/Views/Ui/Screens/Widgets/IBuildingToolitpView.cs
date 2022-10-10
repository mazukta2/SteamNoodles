using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface IBuildingToolitpView : IViewWithGenericPresenter<BuildingTooltipPresenter>, IViewWithDefaultPresenter
    {
        IHandConstructionTooltipView Tooltip { get; }
        IAnimator Animator { get; }

        static IBuildingToolitpView Default { get; set; }
        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this;
            new BuildingTooltipPresenter(this, IModels.Default.Find<PlacementField>());
        }
    }
}
