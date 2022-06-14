using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand
{
    public class HandConstructionTooltipView : ViewWithPresenter<HandConstructionTooltipPresenter>, IHandConstructionTooltipView
    {
        public IText Name { get; } = new TextMock();
        public IText Points { get; } = new TextMock();
        public IText Adjacencies { get; } = new TextMock();

        public HandConstructionTooltipView(IViewsCollection level) : base(level)
        {
        }

        public void Init(ConstructionCard card)
        {
            //new HandConstructionTooltipPresenter(this).SetModel(card);
        }
    }
}
