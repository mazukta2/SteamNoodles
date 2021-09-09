using Assets.Scripts.Logic.Prototypes.Levels;
using System;

namespace Tests.Mocks.Prototypes.Levels
{
    public class BasicLevelPrototype : ILevelPrototype
    {
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
