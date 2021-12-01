using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Game
{
    public interface IGameView
    {
        IGameSessionView Session { get; }
        IGameSessionView CreateSession();
    }
}
