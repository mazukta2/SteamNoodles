using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Tests.Environment.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : PresenterView<HandConstructionPresenter>, IHandConstructionView
    {
        public IButton Button { get; }
        public IImage Image { get; }

        public HandConstructionView(LevelView level, IButton button, IImage view) : base(level)
        {
            Button = button ?? throw new ArgumentNullException(nameof(button));
            Image = view ?? throw new ArgumentNullException(nameof(view));
        }
    }
}
