using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IControls
    {
        void ShakeCamera();
        event Action OnLevelClick;
        event Action<GameVector3> OnLevelPointerMoved;
        event Action<GameKeys> OnTap;
        GameVector3 PointerLevelPosition { get; }
    }
}
