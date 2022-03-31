using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public class UnitView : View
    {
        public ILevelPosition Position { get; }
        public IRotator Rotator { get; }

        private UnitPresenter _presenter;

        public UnitView(ILevel level, ILevelPosition position, IRotator rotator) : base(level)
        {
            Position = position;
            Rotator = rotator;
        }

        public void Init(Unit model)
        {
            _presenter = new UnitPresenter(model, this);
        }
    }
}