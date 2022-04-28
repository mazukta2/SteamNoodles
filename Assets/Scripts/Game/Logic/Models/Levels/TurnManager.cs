using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class TurnManager : Disposable
    {
        public event Action OnTurn = delegate { };

        public void Turn()
        {
            OnTurn();
        }

    }
}
