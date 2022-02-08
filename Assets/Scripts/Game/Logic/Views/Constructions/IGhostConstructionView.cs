using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IGhostConstructionView : IView
    {
        void SetMoveAction(Action<FloatPoint> action);
        void PlaceTo(FloatPoint vector2);
        Action<FloatPoint> GetMoveAction();

        void SetCanBePlacedState(bool value);
        bool GetCanBePlacedState();
        void SetImage(IVisual image);
    }
}
