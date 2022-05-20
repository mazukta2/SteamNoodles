using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public interface IGameSession: IDisposable
    {
        static IGameSession Default { get; set; }

        void StartLastAvailableLevel();
    }
}
