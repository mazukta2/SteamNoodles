using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Common;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Units
{
    public class UnitUnityView : UnityView<UnitPresenter>, IUnitView
    {
        [SerializeField] UnityAnimator _animator;
        [SerializeField] UnitRigs _rigs;
        [SerializeField] UnityPosition _unityPosition;

        public IPosition Position => _unityPosition;
        public IRotator Rotator { get; private set; }
        public IAnimator Animator => _animator;
        public IUnitDresser UnitDresser { get; private set; }

        protected override void PreAwake()
        {
            Rotator = new UnityRotator(transform);
            UnitDresser = new UnitDresser(_rigs, IAssets.Default);
        }

    }

}
