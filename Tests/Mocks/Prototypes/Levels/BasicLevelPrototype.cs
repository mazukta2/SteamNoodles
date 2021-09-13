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

        public IBuildingPrototype[] StartingHand => new IBuildingPrototype[] {
            new BasicBuildingPrototype()
        };

        private Action<ILevelPrototype, ILevelView> _finished;

        public void Finish()
        {
            _finished(this, new BasicLevelView());
        }

        public void Load(Action<ILevelPrototype, ILevelView> onFinished)
        {
            _finished = onFinished;
        }
    }
}
