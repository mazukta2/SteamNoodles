﻿using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels
{
    public interface IScreenView : IView
    {
        DisposableViewKeeper<IClashesView> Clashes { get; }
        DisposableViewKeeper<IHandView> Hand { get; }
        DisposableViewKeeper<ICurrentOrderView> Customers { get; }
        DisposableViewKeeper<ILevelResourcesView> Resources { get; }
    }
}
