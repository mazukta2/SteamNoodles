using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : ViewWithPresenter<HandConstructionPresenter>, IHandConstructionView
    {
        public IButton Button { get; } = new ButtonMock();
        public IImage Image { get; } = new ImageMock();
        public IText Amount { get; } = new TextMock();
        public IAnimator Animator { get; } = new AnimatorMock();
        public IViewContainer TooltipContainer { get; }
        public IViewPrefab TooltipPrefab { get; }

        public HandConstructionView(IViewsCollection collection) : base(collection)
        {
            TooltipContainer = new ContainerViewMock(collection);
            TooltipPrefab = new DefaultViewCollectionPrefabMock(SpawnTooltip);
        }

        private void SpawnTooltip(IViewsCollection collection)
        {
            new HandConstructionTooltipView(collection);
        }
    }
}
