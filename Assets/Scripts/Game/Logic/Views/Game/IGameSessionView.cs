using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Game
{
    public interface IGameSessionView
    {
        ILevelView CurrentLevel { get;}
        void SetLevel(ILevelView levelView);
    }
}
