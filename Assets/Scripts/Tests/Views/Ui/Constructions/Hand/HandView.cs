using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand
{
    public class HandView : ViewWithPresenter<HandPresenter>, IHandView
    {

        public IViewContainer Cards { get; set; }
        public IViewPrefab CardPrototype { get; set; }
        public IButton CancelButton { get; set; } = new ButtonMock();
        public AnimatorMock Animator { get; set; } = new AnimatorMock();
        IAnimator IHandView.Animator => Animator;

        public HandView(IViewsCollection level) : base(level)
        {
            Cards = new ContainerViewMock(level);
            CardPrototype = new DefaultViewCollectionPrefabMock(SpawnCardPrototype);
        }

        private void SpawnCardPrototype(IViewsCollection collection)
        {
            var handTooltipContainer = new ContainerViewMock(collection);
            var handTooltipPrefab = new DefaultViewCollectionPrefabMock(SpawnTooltip);

            new HandConstructionView(collection, new ButtonMock(), new ImageMock(), handTooltipContainer, handTooltipPrefab);
        }

        private void SpawnTooltip(IViewsCollection collection)
        {
            new HandConstructionTooltipView(collection, new TextMock(), new TextMock());
        }
    }
}
