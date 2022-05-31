using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view, IPresenterRepository<Construction> constructions) : base(view)
        {
            _view = view;
            _tooltip = new HandConstructionTooltipPresenter(_view.Tooltip, constructions);
            Hide();
        }

        protected override void DisposeInner()
        {
        }

        public void SetHighlight(IEnumerable<Construction> constructions)
        {
            _tooltip.SetHighlight(constructions.Select(x => x.Scheme));
        }

        public void Show(ConstructionCard constructionCard)
        {
            _tooltip.SetModel(constructionCard);
            _view.Animator.Play(Animations.Show.ToString());
        }

        public void Hide()
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
