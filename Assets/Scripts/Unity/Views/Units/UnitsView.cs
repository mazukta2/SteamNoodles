using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Units
{
    public class UnitsView : GameMonoBehaviour, IUnitsView
    {
        [SerializeField] PrototypeLink _unitPrototype;

        public IUnitView CreateUnit()
        {
            return _unitPrototype.Create<UnitView>();
        }
    }
}