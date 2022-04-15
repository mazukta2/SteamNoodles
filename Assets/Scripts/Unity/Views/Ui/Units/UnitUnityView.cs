using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Common;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Units
{
    public class UnitUnityView : UnityView<UnitPresenter>, IUnitView
    {
        [SerializeField] Animator _animator;
        [SerializeField] UnitRigs _rigs;

        public ILevelPosition Position => new UnityLevelPosition(transform);
        public IRotator Rotator => new UnityRotator(transform);
        public IAnimator Animator => new UnityAnimator(_animator);
        public IUnitDresser UnitDresser => new UnitDresser(_rigs, Level.Engine.Assets);
    }

}
