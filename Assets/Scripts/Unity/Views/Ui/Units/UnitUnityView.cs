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

        public ILevelPosition Position { get; private set; }
        public IRotator Rotator { get; private set; }
        public IAnimator Animator { get; private set; }
        public IUnitDresser UnitDresser { get; private set; }

        protected override void PreAwake()
        {
            Position = new UnityLevelPosition(transform);
            Rotator = new UnityRotator(transform);
            Animator = new UnityAnimator(_animator);
            UnitDresser = new UnitDresser(_rigs, Level.Engine.Assets);
        }

    }

}
