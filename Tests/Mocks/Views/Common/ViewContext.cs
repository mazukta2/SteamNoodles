using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Tests.Mocks.Views.Common;
using System;

namespace Tests.Tests.Mocks.Views.Common
{
    public class ViewContext
    {
        public int Convert(ISprite icon)
        {
            if (icon is ItsUnitySpriteWrapper testSprite)
                return testSprite.Id;

            throw new Exception("Can convert spriteType");
        }
    }
}
