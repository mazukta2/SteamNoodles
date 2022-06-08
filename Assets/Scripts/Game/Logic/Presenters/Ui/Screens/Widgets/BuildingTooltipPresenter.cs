using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private BuildingModeService _buildingModeService;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view) : this(view,
                  IPresenterServices.Default?.Get<IPresenterRepository<Construction>>(),
                  IPresenterServices.Default?.Get<BuildingModeService>())
        {

        }

        public BuildingTooltipPresenter(IBuildingToolitpView view, 
            IPresenterRepository<Construction> constructions, BuildingModeService buildingModeService) : base(view)
        {
            _view = view;
            _buildingModeService = buildingModeService;
            _buildingModeService.OnHighligtingChanged += HandleHighligtingChanged;
            _buildingModeService.OnChanged += HandleOnChanged;
            _tooltip = new HandConstructionTooltipPresenter(_view.Tooltip, constructions);
            Hide();
        }

        protected override void DisposeInner()
        {
            _buildingModeService.OnChanged -= HandleOnChanged;
            _buildingModeService.OnHighligtingChanged -= HandleHighligtingChanged;
        }

        private void HandleHighligtingChanged()
        {
            _tooltip.SetHighlight(_buildingModeService.ConstructionsHighlights.Select(x => x.Scheme));
        }

        private void HandleOnChanged(bool value)
        {
            if (value)
                Show(_buildingModeService.Card);
            else
                Hide();
        }

        private void Show(ConstructionCard constructionCard)
        {
            _tooltip.SetModel(constructionCard);
            _view.Animator.Play(Animations.Show.ToString());
        }

        private void Hide()
        {
            _view.Animator.Play(Animations.Hide.ToString());
        }

        public enum Animations
        {
            Show,
            Hide
        }
    }
}
