using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Units
{
    public interface IClashesView : IView
    {
        IButtonView StartClash { get; }
    }
}
