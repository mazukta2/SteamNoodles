using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Constructions.Hand
{
    public class HandViewPresenter : ViewPresenter
    {
        public ContainerViewPresenter Cards { get; private set; }
        public PrototypeViewPresenter CardPrototype { get; private set; }

        private HandPresenter _presenter;
        public HandViewPresenter(ILevel level, ScreenManagerViewPresenter screenManager, ContainerViewPresenter cards, PrototypeViewPresenter cardPrototype) : base(level)
        {
            Cards = cards ?? throw new ArgumentNullException(nameof(cards));
            CardPrototype = cardPrototype ?? throw new ArgumentNullException(nameof(cardPrototype));

            _presenter = new HandPresenter(level.Model.Hand, screenManager.GetPresenter(), this);
        }
    }
}
