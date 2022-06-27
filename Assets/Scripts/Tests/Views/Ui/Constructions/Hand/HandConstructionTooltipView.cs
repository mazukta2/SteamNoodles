using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;
using System;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

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

        public void Init(Uid cardId)
        {
        }
    }
}
