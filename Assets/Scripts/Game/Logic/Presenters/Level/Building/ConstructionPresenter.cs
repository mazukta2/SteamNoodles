using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter
    {
        private Construction _construction;
        private ConstructionView _constructionView;

        public ConstructionPresenter(Construction construction, ConstructionView view) : base(view)
        {
            _construction = construction;
            _constructionView = view;
            _constructionView.LocalPosition.Value = construction.GetLocalPosition();
        }
    }
}
