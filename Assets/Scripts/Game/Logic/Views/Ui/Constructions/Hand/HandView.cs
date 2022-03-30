﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandView : View
    {
        public TestContainerView Cards { get; private set; }
        public TestPrototypeView CardPrototype { get; private set; }

        private HandPresenter _presenter;
        public HandView(ILevel level, TestContainerView cards, TestPrototypeView cardPrototype) : base(level)
        {
            Cards = cards ?? throw new ArgumentNullException(nameof(cards));
            CardPrototype = cardPrototype ?? throw new ArgumentNullException(nameof(cardPrototype));
        }

        public void SetScreen(BaseGameScreenPresenter screenPresenter)
        {
            _presenter = new HandPresenter(Level.Model.Hand, screenPresenter.Manager, this);
        }
    }
}
