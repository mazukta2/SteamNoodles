using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public class UnitsQueueView : View
    {
        public ILevelPosition StartPosition { get; private set; }

        private UnitsQueuePresenter _presenter;

        public UnitsQueueView(ILevel level, ILevelPosition position) : base(level)
        {
            StartPosition = position;
            _presenter = new UnitsQueuePresenter(this);
        }
    }
}