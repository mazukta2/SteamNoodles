using Game.Assets.Scripts.Game.Environment.Engine;
using System;

namespace Game.Assets.Scripts.Game.External
{
    public interface IGameEngine
    {
        ILevelsManager Levels { get; }
        IDefinitions Settings { get; }
        IAssets Assets { get; }
        void SetTimeMover(Action<float> moveTime);
    }
}
