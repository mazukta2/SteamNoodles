﻿using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Units
{
    public class UnitView : ViewMonoBehaviour, IUnitView
    {
        public void SetPosition(FloatPoint position)
        {
            transform.position = position.ToUnityVector(transform.position.y);
        }
    }
}