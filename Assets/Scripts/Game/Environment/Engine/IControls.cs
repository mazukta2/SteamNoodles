using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IControls
    {
        void ShakeCamera();
        event Action OnLevelClick;
        event Action<GameVector3> OnLevelPointerMoved;
        GameVector3 PointerLevelPosition { get; }
    }
}
