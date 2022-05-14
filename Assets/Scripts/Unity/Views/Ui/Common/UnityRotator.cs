using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    public class UnityRotator : IRotator
    {
        private Transform _transform;
        private GameQuaternion _direction;

        public UnityRotator(Transform transform)
        {
            _transform = transform;
        }

        public GameQuaternion Rotation { get => _direction; set => SetDirection(value); }

        public void SetDirection(GameQuaternion direction)
        {
            if (_transform == null)
                throw new System.Exception("wtf");

            _transform.rotation = new Quaternion(direction.X, direction.Y, direction.Z, direction.W);
            _direction = direction;
        }
    }
}
