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
    public class PositionUnity : IPosition
    {
        [SerializeField] private Transform _transform;

        public GameVector3 Value { get => Get(); set => Set(value); }

        private GameVector3 Get()
        {
            return new GameVector3(_transform.position.x, _transform.position.y, _transform.position.z);
        }

        public void Set(GameVector3 floatPoint)
        {
            _transform.SetPosition(floatPoint);
        }
    }
}
