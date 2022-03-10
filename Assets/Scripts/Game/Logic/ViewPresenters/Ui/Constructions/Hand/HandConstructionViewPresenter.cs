using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Constructions.Hand
{
    public class HandConstructionViewPresenter : ViewPresenter
    {
        public ButtonViewPresenter Button { get; }

        private HandConstructionPresenter _presenter;

        public HandConstructionViewPresenter(ILevel level, ButtonViewPresenter button) : base(level)
        {
            Button = button ?? throw new ArgumentNullException(nameof(button));
        }

        public void Init(ScreenManagerPresenter manager, ConstructionCard construction)
        {
            _presenter = new HandConstructionPresenter(manager, this, construction);
        }
    }
}
