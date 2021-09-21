using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Tests.Mocks.Views.Levels;

namespace Tests.Mocks.Prototypes.Levels
{
    public class BasicLevelPrototype : ILevelPrototype
    {
        public Point Size => new Point(4, 4);

        public IConstructionPrototype[] StartingHand => new IConstructionPrototype[] {
            new BasicBuildingPrototype()
        };

        public IOrderPrototype[] Orders { get; } = new IOrderPrototype[] {
            new BasicOrderPrototype()
        };
        public BasicLevelView Level { get; set; }

        private Action<ILevelPrototype, ILevelView> _finished;

        public void Finish()
        {
            Level = new BasicLevelView();
            _finished(this, Level);
        }

        public void Load(Action<ILevelPrototype, ILevelView> onFinished)
        {
            _finished = onFinished;
        }

    }
}
