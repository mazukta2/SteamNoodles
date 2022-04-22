using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Controls
{
    public interface IGameKeysManager
    {
        static IGameKeysManager Default { get; set; }
        KeyCommand GetKey(GameKeys rotateLeft);
    }
}
