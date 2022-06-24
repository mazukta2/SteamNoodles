using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view) : this(view,
                  IPresenterServices.Default?.Get<IQuery<Construction>>(),
                  IPresenterServices.Default?.Get<ISingletonRepository<ConstructionGhost>>().AsQuery())
        {

        }

        public BuildingTooltipPresenter(IBuildingToolitpView view, 
            IQuery<Construction> constructions, 
            ISingleQuery<ConstructionGhost> ghost) : base(view)
        {
            _view = view;
            _ghost = ghost;
            // _ghostService.OnHighlightingChanged += HandleHighlightingChanged
            _ghost.OnAdded += HandleOnGhostShowed;
            _ghost.OnRemoved += HandleOnGhostHided;
            _tooltip = new HandConstructionTooltipPresenter(_view.Tooltip, constructions);
            Hide();
        }

        protected override void DisposeInner()
        {
            _ghost.Dispose();
            _ghost.OnAdded -= HandleOnGhostShowed;
            _ghost.OnRemoved -= HandleOnGhostHided;
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
            Show(_ghost.Get().Card);
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
