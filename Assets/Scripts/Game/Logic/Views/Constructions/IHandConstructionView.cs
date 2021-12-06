using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Tests.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IHandConstructionView : IView
    {
        void SetIcon(ISprite icon);
        ISprite GetIcon();
        IButtonView Button { get; }
    }
}
