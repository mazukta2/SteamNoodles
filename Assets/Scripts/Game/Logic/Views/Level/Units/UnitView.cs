using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class UnitView : View
    {
        public ContainerView Container { get; private set; }
        public PrototypeView UnitPrototype { get; private set; }

        private UnitPresenter _presenter;

        public UnitView(ILevel level): base(level)
        {
            _presenter = new UnitPresenter();
        }
    }
}