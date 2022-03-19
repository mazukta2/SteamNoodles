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
    public class UnityLevelPosition : ILevelPosition
    {
        private FloatPoint _position;
        private Transform _transform;

        public UnityLevelPosition(Transform transform)
        {
            _transform = transform;
        }

        public FloatPoint Value { get => _position; set => Set(value); }

        public void Set(FloatPoint floatPoint)
        {
            _position = floatPoint;
            _transform.SetPosition(floatPoint);
        }
    }
}
