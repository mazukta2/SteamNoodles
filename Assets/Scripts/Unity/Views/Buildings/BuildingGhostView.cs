using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingGhostView : ViewMonoBehaviour, IGhostConstructionView
    {
        [SerializeField] Color _activeColor;
        [SerializeField] Color _notActiveColor;
        [SerializeField] private UnityEngine.Vector3 _offset;

        private bool _canBePlaced;
        private UnityView _image;
        private Action<FloatPoint> _move;

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

        public Action<FloatPoint> GetMoveAction()
        {
            return _move;
        }

        public void PlaceTo(FloatPoint pos)
        {
            transform.position = pos.ToUnityVector(transform.position.z) + _offset;
        }

        public void SetMoveAction(Action<FloatPoint> action)
        {
            _move = action;
        }

        protected void Update()
        {
            _move(_inputs.GetMouseWorldPosition());
        }

        private void UpdateView()
        {
            //var sprite = GetComponentInChildren<SpriteRenderer>();
            //if (sprite != null)
            //    sprite.color = _canBePlaced ? _activeColor : _notActiveColor;
        }

        public void SetImage(IVisual image)
        {
            _image = new UnityView(image);
            Instantiate(_image.View, transform, false);
        }
    }
}