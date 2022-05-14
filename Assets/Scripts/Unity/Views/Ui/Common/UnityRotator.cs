using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    public class UnityRotator : IRotator
    {
        private Transform _transform;
        private FloatPoint3D _direction;

        public UnityRotator(Transform transform)
        {
            _transform = transform;
        }

        public FloatPoint3D GetDirection()
        {
            return _direction;
        }

        public void LookAtDirection(FloatPoint3D direction)
        {
            if (_transform == null)
                throw new System.Exception("wtf");

            _transform.LookAt(_transform.position + direction.ToVector());
            _direction = direction;
        }
    }
}
