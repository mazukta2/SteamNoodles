using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class UnitView : View
    {
        public ContainerView Container { get; private set; }
        public PrototypeView UnitPrototype { get; private set; }

        private UnitPresenter _presenter;

        public UnitView(ILevel level): base(level)
        {
        }

        public void Init(Unit model)
        {
            _presenter = new UnitPresenter(model, this);
        }

        public void SetPosition(FloatPoint position)
        {
        }
    }
}