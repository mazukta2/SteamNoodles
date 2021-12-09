using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Tests.Mocks.Views.Common;
using NUnit.Framework.Constraints;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Levels
{
    public class BasicHandConstructionView : TestView, IHandConstructionView
    {
        public int SpriteId;
        public IButtonView Button { get; } = new ButtonView();

        public void SetIcon(ISprite icon)
        {
            SpriteId = new ViewContext().Convert(icon);
        }

        public ISprite GetIcon()
        {
            return new ItsUnitySpriteWrapper(SpriteId);
        }

        protected override void DisposeInner()
        {
            Button.Dispose();
        }
    }
}