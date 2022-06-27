using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandConstructionTooltipView : IViewWithPresenter
    {
        IText Name { get; }
        IText Points { get; }
        IText Adjacencies { get; }

        void Init(Uid cardId)
        {
            new HandConstructionTooltipPresenter(this).SetModel(cardId);
        }
    }
}
