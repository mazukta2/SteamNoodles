﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
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
    public class UnityRotator : IRotator
    {
        private Transform _transform;

        public UnityRotator(Transform transform)
        {
            _transform = transform;
        }

        public void FaceTo(FloatPoint value)
        {
            _transform.LookAt(new Vector3(value.X, _transform.position.y, value.Y));
        }

        public void Look(FloatPoint direction)
        {
            _transform.LookAt(new Vector3(_transform.position.x + direction.X, _transform.position.y, _transform.position.z + direction.Y));
        }
    }
}
