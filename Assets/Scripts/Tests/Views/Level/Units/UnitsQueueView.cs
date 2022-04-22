using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;

namespace Game.Assets.Scripts.Tests.Views.Level.Units
{
    public class UnitsQueueView : PresenterView<UnitsQueuePresenter>, IUnitsQueueView
    {
        public ILevelPosition StartPosition { get; private set; }

        public UnitsQueueView(LevelView level, ILevelPosition position) : base(level)
        {
            StartPosition = position;
        }
    }
}