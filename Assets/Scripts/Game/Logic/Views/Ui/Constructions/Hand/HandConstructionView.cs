using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Tests.Environment.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : View
    {
        public IButton Button { get; }
        public IImage Image { get; }

        private HandConstructionPresenter _presenter;

        public HandConstructionView(ILevel level, IButton button, IImage view) : base(level)
        {
            Button = button ?? throw new ArgumentNullException(nameof(button));
            Image = view ?? throw new ArgumentNullException(nameof(view));
        }

        public void Init(ScreenManagerPresenter manager, ConstructionCard construction)
        {
            _presenter = new HandConstructionPresenter(manager, this, construction);
        }
    }
}
