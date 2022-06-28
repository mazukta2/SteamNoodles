using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private readonly GhostRepository _ghost;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view) : this(view,
                  IPresenterServices.Default?.Get<ConstructionsRepository>(),
                  IPresenterServices.Default?.Get<GhostRepository>())
        {

        }

        public BuildingTooltipPresenter(IBuildingToolitpView view, 
            ConstructionsRepository constructions, 
            GhostRepository ghost) : base(view)
        {
            _view = view;
            _ghost = ghost;
            // _ghostService.OnHighlightingChanged += HandleHighlightingChanged
            // _ghost.OnAdded += HandleOnGhostShowed;
            // _ghost.OnRemoved += HandleOnGhostHided;
            _tooltip = new HandConstructionTooltipPresenter(_view.Tooltip, constructions);
            Hide();
        }

        protected override void DisposeInner()
        {
            // _ghost.OnAdded -= HandleOnGhostShowed;
            // _ghost.OnRemoved -= HandleOnGhostHided;
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
            // Show(_ghost.Get().CardId);
        }

        private void Show(Uid constructionCardId)
        {
            _tooltip.SetModel(constructionCardId);
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
