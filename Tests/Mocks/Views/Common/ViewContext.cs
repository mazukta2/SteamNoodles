using Game.Tests.Mocks.Views.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

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
