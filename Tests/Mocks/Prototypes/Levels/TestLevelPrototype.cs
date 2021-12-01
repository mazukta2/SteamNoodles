using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Tests.Mocks.Views.Levels;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Tests.Mocks.Prototypes.Levels
{
    public class TestLevelPrototype : ILevelSettings
    {
        public Point Size => new Point(4, 4);

        public IConstructionSettings[] StartingHand => new IConstructionSettings[] {
            new TestBuildingPrototype()
        };

        public IOrderSettings[] Orders => _orders.ToArray();
        public BasicLevelView Level { get; set; }

        private Action<ILevelSettings, ILevelView> _finished;
        private List<IOrderSettings> _orders = new List<IOrderSettings>();

        public void Finish()
        {
            Level = new BasicLevelView();
            _finished(this, Level);
        }

        public void Load(Action<ILevelSettings, ILevelView> onFinished)
        {
            _finished = onFinished;
        }

        public void Add(IOrderSettings order)
        {
            _orders.Add(order);
        }

        public Rect UnitsSpawnRect { get; set; } = new Rect(-5, -5, 10, 10);
    }
}
