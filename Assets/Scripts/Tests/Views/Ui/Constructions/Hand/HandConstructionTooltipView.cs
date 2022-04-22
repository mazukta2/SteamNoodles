using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand
{
    public class HandConstructionTooltipView : PresenterView<HandConstructionTooltipPresenter>, IHandConstructionTooltipView
    {
        public IText Name { get; }
        public IText Points { get; }
        public IText Adjacencies { get; }

        public HandConstructionTooltipView(LevelView level, IText name, IText points) : base(level)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Points = points ?? throw new ArgumentNullException(nameof(points));
            Adjacencies = new UiText();
        }
    }
}
