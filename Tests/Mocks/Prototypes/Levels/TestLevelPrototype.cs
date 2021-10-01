using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Tests.Mocks.Views.Levels;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Tests.Mocks.Prototypes.Levels
{
    public class TestLevelPrototype : ILevelPrototype
    {
        public Point Size => new Point(4, 4);

        public IConstructionPrototype[] StartingHand => new IConstructionPrototype[] {
            new TestBuildingPrototype()
        };

        public IOrderPrototype[] Orders => _orders.ToArray();
        public BasicLevelView Level { get; set; }

        private Action<ILevelPrototype, ILevelView> _finished;
        private List<IOrderPrototype> _orders = new List<IOrderPrototype>();

        public void Finish()
        {
            Level = new BasicLevelView();
            _finished(this, Level);
        }

        public void Load(Action<ILevelPrototype, ILevelView> onFinished)
        {
            _finished = onFinished;
        }

        public void Add(IOrderPrototype order)
        {
            _orders.Add(order);
        }
    }
}
