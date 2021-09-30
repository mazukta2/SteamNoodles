using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IGhostConstructionView : IView
    {
        void SetMoveAction(Action<Vector2> action);
        void PlaceTo(Vector2 vector2);
        Action<Vector2> GetMoveAction();

        void SetCanBePlacedState(bool value);
        bool GetCanBePlacedState();
        void SetImage(IVisual image);
    }
}
