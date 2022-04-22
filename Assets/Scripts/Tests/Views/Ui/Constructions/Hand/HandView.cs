using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand
{
    public class HandView : PresenterView<HandPresenter>, IHandView
    {
        public IViewContainer Cards { get; private set; }
        public IViewPrefab CardPrototype { get; private set; }
        public HandView(LevelView level, IViewContainer cards, IViewPrefab cardPrototype) : base(level)
        {
            Cards = cards ?? throw new ArgumentNullException(nameof(cards));
            CardPrototype = cardPrototype ?? throw new ArgumentNullException(nameof(cardPrototype));
        }
    }
}
