using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public class UnitsQueueView : PresenterView<UnitsQueuePresenter>
    {
        public ILevelPosition StartPosition { get; private set; }

        public UnitsQueueView(ILevel level, ILevelPosition position) : base(level)
        {
            StartPosition = position;
            new UnitsQueuePresenter(this);
        }
    }
}