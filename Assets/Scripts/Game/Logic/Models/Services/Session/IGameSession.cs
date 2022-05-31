using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Session
{
    public interface IGameSession : IDisposable
    {
        static IGameSession Default { get; set; }

        void StartLastAvailableLevel();
    }
}
