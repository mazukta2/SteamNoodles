using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : PresenterView<HandConstructionPresenter>
    {
        public IButton Button { get; }
        public IImage Image { get; }

        public HandConstructionView(ILevel level, IButton button, IImage view) : base(level)
        {
            Button = button ?? throw new ArgumentNullException(nameof(button));
            Image = view ?? throw new ArgumentNullException(nameof(view));
        }
    }
}
