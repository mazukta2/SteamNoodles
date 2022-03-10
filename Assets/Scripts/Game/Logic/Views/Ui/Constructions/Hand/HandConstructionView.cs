using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : View
    {
        public ButtonView Button { get; }

        private HandConstructionPresenter _presenter;

        public HandConstructionView(ILevel level, ButtonView button) : base(level)
        {
            Button = button ?? throw new ArgumentNullException(nameof(button));
        }

        public void Init(ScreenManagerPresenter manager, ConstructionCard construction)
        {
            _presenter = new HandConstructionPresenter(manager, this, construction);
        }
    }
}
