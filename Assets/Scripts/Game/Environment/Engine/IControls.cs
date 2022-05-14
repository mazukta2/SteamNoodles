using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IControls
    {
        void ShakeCamera();
        event Action OnLevelClick;
        event Action<FloatPoint3D> OnLevelPointerMoved;
        FloatPoint3D PointerLevelPosition { get; }
        static IControls Default { get; set; }
    }
}
