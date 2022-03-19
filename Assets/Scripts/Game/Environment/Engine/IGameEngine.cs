using Game.Assets.Scripts.Game.Environment.Engine;
using System;

namespace Game.Assets.Scripts.Game.External
{
    public interface IGameEngine
    {
        ILevelsManager Levels { get; }
        IDefinitions Definitions { get; }
        IAssets Assets { get; }
        IControls Controls { get; }
    }
}
