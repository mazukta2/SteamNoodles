using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels
{
    public interface IScreenView : IView
    {
        DisposableViewKeeper<IClashesView> Clashes { get; }
        DisposableViewKeeper<IHandView> Hand { get; }
        DisposableViewKeeper<ICurrentOrderView> Order { get; }
    }
}
