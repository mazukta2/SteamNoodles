using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface IBuildingToolitpView : IViewWithGenericPresenter<BuildingTooltipPresenter>, IViewWithDefaultPresenter
    {
        IHandConstructionTooltipView Tooltip { get; }
        IAnimator Animator { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new BuildingTooltipPresenter(this);
        }
    }
}
