using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private GhostService _ghostService;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view) : this(view,
                  IPresenterServices.Default?.Get<IPresenterRepository<Construction>>(),
                  IPresenterServices.Default?.Get<GhostService>())
        {

        }

        public BuildingTooltipPresenter(IBuildingToolitpView view, 
            IPresenterRepository<Construction> constructions, GhostService ghostService) : base(view)
        {
            _view = view;
            _ghostService = ghostService;
            // _ghostService.OnHighlightingChanged += HandleHighlightingChanged
            _ghostService.OnShowed += HandleOnGhostShowed;
            _ghostService.OnHided += HandleOnGhostHided;
            _tooltip = new HandConstructionTooltipPresenter(_view.Tooltip, constructions);
            Hide();
        }

        protected override void DisposeInner()
        {
            _ghostService.OnShowed -= HandleOnGhostShowed;
            _ghostService.OnHided -= HandleOnGhostHided;
            // _ghostService.OnHighlightingChanged -= HandleHighlightingChanged;
        }

        private void HandleHighlightingChanged()
        {
            // _tooltip.SetHighlight(_ghostService.GetConstructionsHighlights().Select(x => x.Scheme));
        }

        private void HandleOnGhostHided()
        {
            Hide();
        }

        private void HandleOnGhostShowed()
        {
            Show(_ghostService.GetGhost().Card);
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
