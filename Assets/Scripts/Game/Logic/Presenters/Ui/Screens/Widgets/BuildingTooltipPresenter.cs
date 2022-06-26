using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private readonly IDataProvider<GhostData> _ghost;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view) : this(view,
                  IPresenterServices.Default?.Get<IDataCollectionProviderService<ConstructionData>>().Get(),
                  IPresenterServices.Default?.Get<IDataProviderService<GhostData>>().Get())
        {

        }

        public BuildingTooltipPresenter(IBuildingToolitpView view, 
            IDataCollectionProvider<ConstructionData> constructions, 
            IDataProvider<GhostData> ghost) : base(view)
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
            Show(_ghost.Get().CardData);
        }

        private void Show(IDataProvider<ConstructionCardData> constructionCard)
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
