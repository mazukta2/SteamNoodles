using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Tests.Tests.Mocks.Views.Common
{
    public class ItsUnitySpriteWrapper : ISprite, IVisual
    {
        public int Id { get; }
        private static int _counter = 1;

        public ItsUnitySpriteWrapper()
        {
            Id = _counter++;
        }

        public ItsUnitySpriteWrapper(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is ItsUnitySpriteWrapper wr)
                return wr.Id == Id;
            return base.Equals(obj);
        }
    }
}
