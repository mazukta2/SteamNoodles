using Assets.Scripts.Models.Buildings;
using System;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface IGhostConstructionView : IView
    {
        void SetMoveAction(Action<Vector2> action);
        void MoveTo(Vector2 vector2);
        Action<Vector2> GetMoveAction();

        void SetCanBePlacedState(bool value);
        bool GetCanBePlacedState();
    }
}
