using Assets.Scripts.Core;
using System;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingGhostView : GameMonoBehaviour, IGhostConstructionView
    {
        [SerializeField] Color _activeColor;
        [SerializeField] Color _notActiveColor;

        private bool _canBePlaced;
        private Action<System.Numerics.Vector2> _move;

        private GameInputs _inputs = new GameInputs();

        protected void Awake()
        {
            UpdateView();
        }

        public bool GetCanBePlacedState()
        {
            return _canBePlaced;
        }

        public void SetCanBePlacedState(bool value)
        {
            _canBePlaced = value;
            UpdateView();
        }

        public Action<System.Numerics.Vector2> GetMoveAction()
        {
            return _move;
        }

        public void PlaceTo(System.Numerics.Vector2 vector2)
        {
            transform.position = new UnityEngine.Vector3(vector2.X, vector2.Y, transform.position.z);
        }

        public void SetMoveAction(Action<System.Numerics.Vector2> action)
        {
            _move = action;
        }

        protected void Update()
        {
            _move(_inputs.GetMouseWorldPosition());
        }

        private void UpdateView()
        {
            GetComponentInChildren<SpriteRenderer>().color = _canBePlaced ? _activeColor : _notActiveColor;
        }

    }
}