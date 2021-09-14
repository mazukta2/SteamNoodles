using NUnit.Framework.Constraints;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicHandConstructionView : TestView, IHandConstructionView
    {
        public int SpriteId;
        private Action _clickAction;

        public void SetIcon(ISprite icon)
        {
            SpriteId = new ViewContext().Convert(icon);
        }

        public ISprite GetIcon()
        {
            return new ItsUnitySpriteWrapper(SpriteId);
        }

        public void SetClick(Action action)
        {
            _clickAction = action;
        }

        public void Click()
        {
            _clickAction();
        }
    }
}