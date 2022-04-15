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
    public class UnitUnityView : UnityView<UnitView>
    {
        [SerializeField] Animator _animator;
        [SerializeField] UnitRigs _rigs;

        protected override UnitView CreateView()
        {
            return new UnitView(Level, new UnityLevelPosition(transform), 
                new UnityRotator(transform), new UnityAnimator(_animator), 
                new UnitDresser(_rigs, Level.Engine.Assets));
        }

    }

}
