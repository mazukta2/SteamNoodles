using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;

namespace Tests.Mocks.Prototypes.Levels
{
    public class BasicLevelPrototype : ILevelPrototype
    {
        public Point Size => new Point(4, 4);

        public IBuildingPrototype[] StartingHand => new IBuildingPrototype[] {
            new BasicBuildingPrototype()
        };

        private Action<ILevelPrototype> _finished;

        public void Load(Action<ILevelPrototype> onFinished)
        {
            _finished = onFinished;
        }

        public void Finish()
        {
            _finished(this);
        }
    }
}
