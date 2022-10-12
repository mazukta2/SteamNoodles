using System;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.StartMenuLevels
{
    public interface IStartNewGameView : IView
    {
        IButton Button { get; }
    }
}

