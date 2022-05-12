using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Common;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : ViewWithPresenter<HandConstructionPresenter>, IHandConstructionView
    {
        public IButton Button { get; }
        public IImage Image { get; }
        public IViewContainer TooltipContainer { get; }
        public IViewPrefab TooltipPrefab { get; }
        public IText Amount { get; } = new TextMock();
        public IAnimator Animator { get; } = new AnimatorMock();

        public HandConstructionView(IViewsCollection level, IButton button, IImage view, IViewContainer tooltipContainer, IViewPrefab tooltipPrefab) : base(level)
        {
            Button = button ?? throw new ArgumentNullException(nameof(button));
            Image = view ?? throw new ArgumentNullException(nameof(view));
            TooltipContainer = tooltipContainer;
            TooltipPrefab = tooltipPrefab;
        }
    }
}
