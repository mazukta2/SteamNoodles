using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandConstructionView : IViewWithPresenter
    {
        IButton Button { get; }
        IImage Image { get; }
        IViewContainer TooltipContainer { get; }
        IViewPrefab TooltipPrefab { get; }
        IText Amount { get; }
        IAnimator Animator { get;}

        void Init(ConstructionCard card)
        {
            new HandConstructionPresenter(this, card);
        }
    }
}
