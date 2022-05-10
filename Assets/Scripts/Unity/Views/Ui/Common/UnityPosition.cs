using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    [Serializable]
    public class UnityPosition : IPosition
    {
        [SerializeField] private Transform _transform;

        public FloatPoint3D Value { get => Get(); set => Set(value); }

        private FloatPoint3D Get()
        {
            return new FloatPoint3D(_transform.position.x, _transform.position.y, _transform.position.z);
        }

        public void Set(FloatPoint3D floatPoint)
        {
            _transform.SetPosition(floatPoint);
        }
    }
}
