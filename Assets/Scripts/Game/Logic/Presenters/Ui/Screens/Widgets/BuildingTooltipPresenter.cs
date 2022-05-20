using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class BuildingTooltipPresenter : BasePresenter<IBuildingToolitpView>
    {
        private IBuildingToolitpView _view;
        private PlacementField _field;
        private HandConstructionTooltipPresenter _tooltip;

        public BuildingTooltipPresenter(IBuildingToolitpView view, PlacementField field) : base(view)
        {
            _view = view;
            _field = field;
            _tooltip = new HandConstructionTooltipPresenter(_view.Tooltip, _field);
            Hide();
        }

        protected override void DisposeInner()
        {
        }

        public void SetHighlight(IEnumerable<Construction> constructions)
        {
            _tooltip.SetHighlight(constructions.Select(x => x.Definition));
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
