using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public interface IGame : IDisposable
    {
        event Action OnLevelDestroyed;
        event Action<ILevel> OnLevelCreated;
        event Action OnDispose;
        void Exit();
        void StartNewGame();


        static IGame Default { get; set; }
    }
}
