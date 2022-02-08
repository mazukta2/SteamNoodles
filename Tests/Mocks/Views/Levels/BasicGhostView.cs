using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System;
using System.Numerics;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicGhostView : TestView, IGhostConstructionView
    {
        public FloatPoint Position;

        private Action<FloatPoint> _move;
        private bool _canBePlaced;
        private IVisual _image;

        public void SetMoveAction(Action<FloatPoint> action)
        {
            _move = action;
        }

        public Action<FloatPoint> GetMoveAction()
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

        public void PlaceTo(FloatPoint vector2)
        {
            Position = vector2;
        }

        public void SetImage(IVisual image)
        {
            _image = image;
        }
        protected override void DisposeInner()
        {
        }
    }
}
