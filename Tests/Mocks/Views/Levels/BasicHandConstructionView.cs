using NUnit.Framework.Constraints;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicHandConstructionView : IHandConstructionView
    {
        public int SpriteId;

        public void SetIcon(ISprite icon)
        {
            SpriteId = new SpriteViewContext().Convert(icon);
        }

        public ISprite GetIcon()
        {
            return new ItsUnitySpriteWrapper(SpriteId);
        }
    }
}