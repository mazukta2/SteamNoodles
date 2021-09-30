using Game.Assets.Scripts.Game.Logic.Views;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Tests.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IHandConstructionView : IView
    {
        void SetIcon(ISprite icon);
        ISprite GetIcon();
        void SetClick(Action action);
        void Click();
    }
}
