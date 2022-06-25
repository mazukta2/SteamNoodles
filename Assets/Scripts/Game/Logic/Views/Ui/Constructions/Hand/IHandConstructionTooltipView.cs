using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandConstructionTooltipView : IViewWithPresenter
    {
        IText Name { get; }
        IText Points { get; }
        IText Adjacencies { get; }

        void Init(ConstructionCard card)
        {
            new HandConstructionTooltipPresenter(this).SetModel(card);
        }
    }
}
