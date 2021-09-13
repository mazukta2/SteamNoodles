using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Tests.Tests.Mocks.Views.Common
{
    public class ItsUnitySpriteWrapper : ISprite
    {
        public int Id { get; }
        private static int _counter;

        public ItsUnitySpriteWrapper()
        {
            Id = _counter++;
        }

    }
}
