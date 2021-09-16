using Assets.Scripts.Core;
using System;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingGhostView : GameMonoBehaviour, IGhostConstructionView
    {
        private bool _canBePlaced;
        private Action<Vector2> _move;

        private GameInputs _inputs = new GameInputs();

        public bool GetCanBePlacedState()
        {
            return _canBePlaced;
        }

        public void SetCanBePlacedState(bool value)
        {
            _canBePlaced = value;
        }

        public Action<Vector2> GetMoveAction()
        {
            return _move;
        }

        public void PlaceTo(Vector2 vector2)
        {
            transform.position = new UnityEngine.Vector3(vector2.X, vector2.Y, transform.position.z);
        }

        public void SetMoveAction(Action<Vector2> action)
        {
            _move = action;
        }

        protected void Update()
        {
            _move(_inputs.GetMouseWorldPosition());
        }
    }
}