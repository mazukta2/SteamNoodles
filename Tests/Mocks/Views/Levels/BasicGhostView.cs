using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicGhostView : TestView, IGhostConstructionView
    {
        public Vector2 Position;

        private Action<Vector2> _move;
        private bool _canBePlaced;

        public void SetMoveAction(Action<Vector2> action)
        {
            _move = action;
        }

        public Action<Vector2> GetMoveAction()
        {
            return _move;
        }

        public void SetCanBePlacedState(bool value)
        {
            _canBePlaced = value;
        }

        public bool GetCanBePlacedState()
        {
            return _canBePlaced;
        }

        public void PlaceTo(Vector2 vector2)
        {
            Position = vector2;
        }
    }
}
