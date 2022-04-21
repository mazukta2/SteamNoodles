using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Tests.Environment.Views.Ui.Constructions.Hand
{
    public class HandConstructionTooltipView : PresenterView<HandConstructionTooltipPresenter>, IHandConstructionTooltipView
    {
        public IText Name { get; }

        public HandConstructionTooltipView(LevelView level, IText name) : base(level)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
