using Game.Assets.Scripts.Game.Environment.Engine.Controls;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface IControls
    {
        event Action OnLevelClick;
        event Action<FloatPoint> OnLevelPointerMoved;
        GameKeysManager Keys { get; }
    }
}
